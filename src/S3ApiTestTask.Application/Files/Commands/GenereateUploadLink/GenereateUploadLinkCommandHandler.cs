using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using S3ApiTestTask.Application.Common.Configs;
using S3ApiTestTask.Application.Common.Services.CacheRepositories;
using S3ApiTestTask.Application.Common.Static;
using S3ApiTestTask.Contracts.Requests.Files.GenereateUploadLink;

namespace S3ApiTestTask.Application.Files.Commands.GenereateUploadLink;

/// <summary>
/// Обработчик для <see cref="GenereateUploadLinkCommand"/>
/// </summary>
public class GenereateUploadLinkCommandHandler : IRequestHandler<GenereateUploadLinkCommand, GenereateUploadLinkResponse>
{
	private readonly IFileCacheRepository _fileCache;

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="fileCache">Репозиторий кэширования файлов</param>
	public GenereateUploadLinkCommandHandler(
		IFileCacheRepository fileCache)
	{
		_fileCache = fileCache;
	}

	/// <inheritdoc/>
	public async Task<GenereateUploadLinkResponse> Handle(GenereateUploadLinkCommand request, CancellationToken cancellationToken)
	{
		var linkId = Guid.NewGuid();
		await _fileCache.SetEmptyAsync(
			linkId,
			cancellationToken);
		
		// TODO: REDO
		//return new GenereateUploadLinkResponse
		//{
		//	UploadLink = $"/AppFiles/{linkId}"
		//};

		return new GenereateUploadLinkResponse
		{
			UploadLink = $"/AppFiles/Upload/{linkId}"
		};
	}
}
