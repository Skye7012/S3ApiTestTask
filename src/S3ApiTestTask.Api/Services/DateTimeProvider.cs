using S3ApiTestTask.Application.Common.Services;

namespace S3ApiTestTask.Api.Services;

/// <inheritdoc/>
public class DateTimeProvider : IDateTimeProvider
{
	/// <inheritdoc/>
	public DateTime UtcNow => DateTime.UtcNow;
}
