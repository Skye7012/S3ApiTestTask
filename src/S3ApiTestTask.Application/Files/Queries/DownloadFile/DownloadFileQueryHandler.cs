using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S3ApiTestTask.Application.Common.Exceptions;
using S3ApiTestTask.Application.Common.Services;
using S3ApiTestTask.Domain.Entities;

namespace S3ApiTestTask.Application.Files.Queries.DownloadFile;

/// <summary>
/// Обработчик для of <see cref="DownloadFileQuery"/>
/// </summary>
public class DownloadFileQueryHandler : IRequestHandler<DownloadFileQuery, FileStreamResult>
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
	public async Task<FileStreamResult> Handle(DownloadFileQuery request, CancellationToken cancellationToken)
	{
		var file = await _context.Files.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
			?? throw new EntityNotFoundProblem<AppFile>(request.Id);

		var stream = await _s3Service.DownloadAsync(file.Name, cancellationToken);

		return new FileStreamResult(stream, MediaTypeNames.Application.Octet)
		{
			FileDownloadName = file.Name,
		};
	}
}
