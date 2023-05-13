namespace S3ApiTestTask.Contracts.Requests.Files.UploadFile;

/// <summary>
/// Ответ на запрос генерации ссылки на загрузку файла
/// </summary>
public class UploadFileResponse
{
	/// <summary>
	/// Ссылка на скачивание файла
	/// </summary>
	public string DownloadLink { get; set; } = default!;
}
