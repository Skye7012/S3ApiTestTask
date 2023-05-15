namespace S3ApiTestTask.Contracts.Requests.Files.GenereateUploadLink;

/// <summary>
/// Запрос генерации ссылки на загрузку файла
/// </summary>
public class GenereateUploadLinkRequest
{
	/// <summary>
	/// Наименование файла
	/// </summary>
	public string FileName { get; set; } = default!;
}
