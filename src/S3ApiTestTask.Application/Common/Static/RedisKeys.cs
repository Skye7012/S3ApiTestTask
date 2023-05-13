namespace S3ApiTestTask.Application.Common.Static;

/// <summary>
/// Методы для формирования ключей redis
/// </summary>
public class RedisKeys
{
	/// <summary>
	/// Получить ключ для ссылки на загрузку файла
	/// </summary>
	/// <param name="id">Идентификатор ссылки загрузки</param>
	/// <returns>Ключ для ссылки на загрузку файла</returns>
	public static string GetUploadLinkKey(Guid id)
		=> $"uploadLink:{id}";
}
