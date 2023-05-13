using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using S3ApiTestTask.Application.Common.Configs;
using S3ApiTestTask.Application.Common.Services.CacheRepositories;
using S3ApiTestTask.Application.Common.Static;
using S3ApiTestTask.Domain.Exceptions;

namespace S3ApiTestTask.Infrastructure.Services.CacheRepositories;

/// <summary>
/// Репозиторий закэшированных идентификаторов ссылок загрузки файла
/// </summary>
public class FileCacheRepository: IFileCacheRepository
{
	private readonly IDistributedCache _cache;
	private readonly FilesConfig _filesConfig;

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="cache">Сервис кэширования</param>
	/// <param name="filesConfig"></param>
	public FileCacheRepository(
		IDistributedCache cache,
		IOptions<FilesConfig> filesConfig)
	{
		_cache = cache;
		_filesConfig = filesConfig.Value;
	}

	/// <summary>
	/// Получить идентификатор фалйа
	/// </summary>
	/// <param name="id">Идентификатор ссылки загрузки файла</param>
	/// <param name="cancellationToken">Токен отмены</param>
	/// <returns></returns>
	public async Task CheckAsync(
		Guid id,
		CancellationToken cancellationToken)
	{
		var fileID = await _cache.GetStringAsync(
			RedisKeys.GetUploadLinkKey(id),
			cancellationToken);

		// TODO: тест + дать ссылку на скачивание
		if (fileID == null)
			throw new ValidationProblem("Невалидная ссылка для загрузки файла");

		if (fileID != Guid.Empty.ToString())
			throw new ValidationProblem("По этой ссылке уже был загружен файл");
	}

	/// <summary>
	/// Присвоить идентификатор файла идентификатору ссылки загрузки файла
	/// </summary>
	/// <param name="id">Идентификатор ссылки загрузки файла</param>
	/// <param name="value">Идентификатор файла</param>
	/// <param name="cancellationToken">Токен отмены</param>
	/// <returns></returns>
	public async Task SetAsync(
		Guid uploadLinkId,
		Guid fileId,
		CancellationToken cancellationToken)
			=> await _cache.SetStringAsync(
					RedisKeys.GetUploadLinkKey(uploadLinkId),
					fileId.ToString(),
					new DistributedCacheEntryOptions
					{
						AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(_filesConfig.UploadLinkLifetime),
					},
					cancellationToken);

	/// <summary>
	/// Присвоить идентификатор ссылки загрузки файла без значения
	/// </summary>
	/// <param name="uploadLinkId">Идентификатор ссылки загрузки файла</param>
	/// <param name="cancellationToken">Токен отмены</param>
	/// <returns></returns>
	public async Task SetEmptyAsync(
		Guid uploadLinkId,
		CancellationToken cancellationToken)
			=> await SetAsync(uploadLinkId, Guid.Empty, cancellationToken);
}
