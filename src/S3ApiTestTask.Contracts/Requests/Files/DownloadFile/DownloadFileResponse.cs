namespace S3ApiTestTask.Contracts.Requests.Files.DownloadFile;

/// <summary>
/// Ответ на запрос генерации ссылки на загрузку файла
/// </summary>
public class DownloadFileResponse
{
	/// <summary>
	/// Ссылка на скачивание файла
	/// </summary>
	public string DownloadLink { get; set; } = default!;
}
