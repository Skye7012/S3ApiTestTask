namespace S3ApiTestTask.Application.Common.Configs;

/// <summary>
/// Конфигурация S3
/// </summary>
public class S3Config
{
	/// <summary>
	/// Наименование секции в appSettings
	/// </summary>
	public const string ConfigSectionName = "S3Config";

	/// <summary>
	/// Внутренний URL
	/// </summary>
	public string InternalUrl { get; set; } = default!;

	/// <summary>
	/// Внешний URL
	/// </summary>
	public string ExternalUrl { get; set; } = default!;

	/// <summary>
	/// Ключ доступа
	/// </summary>
	public string AccessKey { get; set; } = default!;

	/// <summary>
	/// Секретный ключ
	/// </summary>
	public string SecterKey { get; set; } = default!;

	/// <summary>
	/// Время жизни presigned url в секундах
	/// </summary>
	public int PresignedUrlLifetime { get; set; } = 600;

	/// <summary>
	/// Наименование бакета
	/// </summary>
	public string BucketName { get; set; } = "files";
}
