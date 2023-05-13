namespace S3ApiTestTask.Application.Common.Services;

/// <summary>
/// Мокируемый провайдер для <see cref="DateTime"/>
/// </summary>
public interface IDateTimeProvider
{
	/// <summary>
	/// Мокируемый <see cref="DateTime.UtcNow"/>
	/// </summary>
	DateTime UtcNow { get; }
}
