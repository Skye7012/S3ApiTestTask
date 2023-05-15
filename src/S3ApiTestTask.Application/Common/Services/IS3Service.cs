using S3ApiTestTask.Domain.Entities;

namespace S3ApiTestTask.Application.Common.Services;

/// <summary>
/// Сервис для работы с S3 хранилищем
/// </summary>
public interface IS3Service
{
	/// <summary>
	/// Существует ли файл
	/// </summary>
	/// <param name="fileName">Наименование файла</param>
	/// <param name="cancellationToken">Токен отмены</param>
	/// <returns>Существует ли файл</returns>
	Task<bool> IsObjectExistsAsync(
		string fileName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Истекло ли время для загрузки файла
	/// </summary>
	/// <param name="file">Файл</param>
	/// <returns>Истекло ли время для загрузки файла</returns>
	bool IsFileUploadingTimeExpired(AppFile file);

	/// <summary>
	/// Скачать файл
	/// </summary>
	/// <param name="fileName">Наименование файла</param>
	/// <returns>Файл</returns>
	Task<string> PresignedGetObjectAsync(string fileName);

	/// <summary>
	/// Скачать файл
	/// </summary>
	/// <param name="fileName">Наименование файла</param>
	/// <returns></returns>
	Task<string> PresignedPutObjectAsync(string fileName);

	/// <summary>
	/// Создать бакет, если он не существует
	/// </summary>
	/// <param name="bucketName">Наименование бакета</param>
	/// <param name="cancellationToken">Токен отмены</param>
	/// <returns>Существовал ли бакет</returns>
	Task<bool> CreateBucketIfNotExist(
		string bucketName,
		CancellationToken cancellationToken);
}
