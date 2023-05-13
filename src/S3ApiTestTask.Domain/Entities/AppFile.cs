using S3ApiTestTask.Domain.Entities.Common;

namespace S3ApiTestTask.Domain.Entities;

/// <summary>
/// Файл
/// </summary>
public class AppFile : EntityBase, ISoftDeletable
{
	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="name">Наименование файла</param>
	public AppFile(string name) => Name = name;

	/// <summary>
	/// Конструктор
	/// </summary>
	protected AppFile() { }

	/// <summary>
	/// Наименование файла
	/// </summary>
	public string Name { get; set; } = default!;

	/// <inheritdoc/>
	public DateTime? DeletedOn { get; set; }
}
