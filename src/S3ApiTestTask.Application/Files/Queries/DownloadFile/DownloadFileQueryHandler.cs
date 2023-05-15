using MediatR;
using Microsoft.EntityFrameworkCore;
using S3ApiTestTask.Application.Common.Exceptions;
using S3ApiTestTask.Application.Common.Services;
using S3ApiTestTask.Contracts.Requests.Files.DownloadFile;
using S3ApiTestTask.Domain.Entities;
using S3ApiTestTask.Domain.Exceptions;

namespace S3ApiTestTask.Application.Files.Queries.DownloadFile;

/// <summary>
/// Обработчик для of <see cref="DownloadFileQuery"/>
/// </summary>
public class DownloadFileQueryHandler : IRequestHandler<DownloadFileQuery, DownloadFileResponse>
{
	private readonly IApplicationDbContext _context;
	private readonly IS3Service _s3Service;

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="context">Контекст БД</param>
	/// <param name="s3Service">S3 хранилище</param>
	public DownloadFileQueryHandler(IApplicationDbContext context, IS3Service s3Service)
	{
		_context = context;
		_s3Service = s3Service;
	}

	/// <inheritdoc/>
	public async Task<DownloadFileResponse> Handle(DownloadFileQuery request, CancellationToken cancellationToken)
	{
		var file = await _context.Files
			.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
			?? throw new EntityNotFoundProblem<AppFile>(request.Id);

		if (file.DeletedOn != null)
			throw new ValidationProblem("Файл был удален");

		var isExists = await _s3Service.IsObjectExistsAsync(file.Name, cancellationToken);
		if (!isExists)
		{
			if (!_s3Service.IsFileUploadingTimeExpired(file))
				throw new ValidationProblem("Файл не был загружен");

			_context.Files.Remove(file);
			await _context.SaveChangesAsync(cancellationToken);
			throw new ValidationProblem("Файл был удален, потому что не был загружен в хранилище");
		}

		return new DownloadFileResponse
		{
			DownloadLink = await _s3Service.PresignedGetObjectAsync(file.Name),
		};
	}
}
