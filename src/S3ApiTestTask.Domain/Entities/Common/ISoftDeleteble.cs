namespace S3ApiTestTask.Domain.Entities.Common;

/// <summary>
/// Сущность, поддерживающая мягкое удаление
/// </summary>
public interface ISoftDeletable
{
	/// <summary>
	/// Дата удаления
	/// </summary>
	public DateTime? DeletedOn { get; set; }
}
