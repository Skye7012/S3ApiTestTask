namespace S3ApiTestTask.Contracts.Requests.Files.GenereateUploadLink;

/// <summary>
/// Ответ на запрос генерации ссылки на загрузку файла
/// </summary>
public class GenereateUploadLinkResponse
{
	/// <summary>
	/// Ссылка на загрузку файла
	/// </summary>
	public string UploadLink { get; set; } = default!;

	/// <summary>
	/// Идентификатор файла
	/// </summary>
	public Guid FileId { get; set; }
}
