using MediatR;
using S3ApiTestTask.Application.Common.Services;
using S3ApiTestTask.Contracts.Requests.Files.GenereateUploadLink;
using S3ApiTestTask.Domain.Entities;

namespace S3ApiTestTask.Application.Files.Commands.GenereateUploadLink;

/// <summary>
/// Обработчик для <see cref="GenereateUploadLinkCommand"/>
/// </summary>
public class GenereateUploadLinkCommandHandler : IRequestHandler<GenereateUploadLinkCommand, GenereateUploadLinkResponse>
{
	private readonly IApplicationDbContext _context;
	private readonly IS3Service _s3Service;

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="context">Контекст БД</param>
	/// <param name="s3Service">Хранилище S3</param>
	public GenereateUploadLinkCommandHandler(
		IApplicationDbContext context,
		IS3Service s3Service)
	{
		_context = context;
		_s3Service = s3Service;
	}

	/// <inheritdoc/>
	public async Task<GenereateUploadLinkResponse> Handle(GenereateUploadLinkCommand request, CancellationToken cancellationToken)
	{
		var fileName = $"{Guid.NewGuid()}_{request.FileName}";

		var file = new AppFile(fileName);
		await _context.Files.AddAsync(file, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);

		return new GenereateUploadLinkResponse
		{
			UploadLink = await _s3Service.PresignedPutObjectAsync(fileName),
			FileId = file.Id,
		};
	}
}
