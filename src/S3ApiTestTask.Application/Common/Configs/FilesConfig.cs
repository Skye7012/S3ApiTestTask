namespace S3ApiTestTask.Application.Common.Configs;

/// <summary>
/// Конфигурация для файлов
/// </summary>
public class FilesConfig
{
	/// <summary>
	/// Наименование секции в appSettings
	/// </summary>
	public const string ConfigSectionName = "FilesConfig";

	/// <summary>
	/// Время жизни ссылки на загрузку файла в секундах
	/// </summary>
	public int UploadLinkLifetime { get; set; } = 600;
}
