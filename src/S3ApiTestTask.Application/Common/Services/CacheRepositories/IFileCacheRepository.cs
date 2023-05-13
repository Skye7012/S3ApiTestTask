namespace S3ApiTestTask.Application.Common.Services.CacheRepositories;

/// <summary>
/// Репозиторий закэшированных идентификаторов ссылок загрузки файла
/// </summary>
public interface IFileCacheRepository
{
	/// <summary>
	/// Проверить, что данный идентификатор ссылки загрузки файла существует, 
	/// и файл по данной ссылке ещё не загружали
	/// </summary>
	/// <param name="id">Идентификатор ссылки загрузки файла</param>
	/// <param name="cancellationToken">Токен отмены</param>
	Task CheckAsync(
		Guid id,
		CancellationToken cancellationToken);

	/// <summary>
	/// Присвоить идентификатор файла идентификатору ссылки загрузки файла
	/// </summary>
	/// <param name="uploadLinkId">Идентификатор ссылки загрузки файла</param>
	/// <param name="fileId">Идентификатор файла</param>
	/// <param name="cancellationToken">Токен отмены</param>
	Task SetAsync(
		Guid uploadLinkId,
		Guid fileId,
		CancellationToken cancellationToken);

	/// <summary>
	/// Присвоить идентификатор ссылки загрузки файла без значения
	/// </summary>
	/// <param name="uploadLinkId">Идентификатор ссылки загрузки файла</param>
	/// <param name="cancellationToken">Токен отмены</param>
	Task SetEmptyAsync(
		Guid uploadLinkId,
		CancellationToken cancellationToken);
}
