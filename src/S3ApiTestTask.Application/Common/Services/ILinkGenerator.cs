namespace S3ApiTestTask.Application.Common.Services;

/// <inheritdoc/>
public interface ILinkGenerator
{
	/// <summary>
	/// Загрузить файл
	/// </summary>
	/// <param name="stream">Файл</param>
	/// <param name="fileName">Наименование файла</param>
	/// <param name="cancellationToken">Токен отмены</param>
	/// <returns></returns>
	string GetUploadUrl(
		Stream stream,
		string fileName,
		CancellationToken cancellationToken);
}
