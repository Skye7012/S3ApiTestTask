namespace S3ApiTestTask.Contracts.Requests.Files.Common;

/// <summary>
/// Ответ на запрос получения загруженного файла
/// </summary>
public class GetFileResponse
{
	/// <summary>
	/// Идентификатор
	/// </summary>
	public Guid Id { get; set; }

	/// <summary>
	/// Наименование
	/// </summary>
	public string Name { get; set; } = default!;

	/// <summary>
	/// Дата создания
	/// </summary>
	public DateTime CreateOn { get; set; }

	/// <summary>
	/// Дата удаления
	/// </summary>
	public DateTime? DeletedOn { get; set; }
}
