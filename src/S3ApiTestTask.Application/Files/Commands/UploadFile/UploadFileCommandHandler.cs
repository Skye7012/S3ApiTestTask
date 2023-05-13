using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using S3ApiTestTask.Application.Common.Configs;
using S3ApiTestTask.Application.Common.Extensions;
using S3ApiTestTask.Application.Common.Services;
using S3ApiTestTask.Application.Common.Services.CacheRepositories;
using S3ApiTestTask.Application.Common.Static;
using S3ApiTestTask.Contracts.Requests.Files.UploadFile;
using S3ApiTestTask.Domain.Entities;
using S3ApiTestTask.Domain.Exceptions;

namespace S3ApiTestTask.Application.Files.Commands.UploadFile;

/// <summary>
/// Обработчик для <see cref="UploadFileCommand"/>
/// </summary>
public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, UploadFileResponse>
{
	private readonly IApplicationDbContext _context;
	private readonly IS3Service _s3Service;
	private readonly IFileCacheRepository _fileCache;

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="context">Контекст БД</param>
	/// <param name="s3Service">S3 хранилище</param>
	/// <param name="fileCache">Репозиторий кэширования файлов</param>
	public UploadFileCommandHandler(
		IApplicationDbContext context,
		IS3Service s3Service,
		IFileCacheRepository fileCache)
	{
		_context = context;
		_s3Service = s3Service;
		_fileCache = fileCache;
	}

	/// <inheritdoc/>
	public async Task<UploadFileResponse> Handle(UploadFileCommand request, CancellationToken cancellationToken)
	{
		await _fileCache.CheckAsync(request.Id, cancellationToken);

		var fileName = $"{Guid.NewGuid()}_{request.File.FileName}";

		await _s3Service.UploadAsync(
			request.File.GetStream(),
			fileName,
			cancellationToken);

		var file = new AppFile(fileName);
		await _context.Files.AddAsync(file, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);

		await _fileCache.SetAsync(
			request.Id,
			file.Id,
			cancellationToken);

		// TODO: REDO
		return new UploadFileResponse
		{
			DownloadLink = $"/AppFiles/Upload/{file.Id}"
		};
	}
}
