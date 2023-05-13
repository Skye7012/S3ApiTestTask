using Microsoft.EntityFrameworkCore;
using S3ApiTestTask.Domain.Entities;

namespace S3ApiTestTask.Application.Common.Services;

/// <summary>
/// Интерфейс контекста БД данного приложения
/// </summary>
public interface IApplicationDbContext : IDbContext
{
	/// <summary>
	/// Файлы
	/// </summary>
	DbSet<AppFile> Files { get; }

	/// <summary>
	/// Сохранить изменения
	/// </summary>
	/// <param name="withSoftDelete">Использовать мягкое удаление</param>
	/// <param name="acceptAllChangesOnSuccess"></param>
	/// <returns>Количество записей состояния, записанных в базу данных</returns>
	int SaveChanges(
		bool withSoftDelete = true,
		bool acceptAllChangesOnSuccess = true);

	/// <summary>
	/// Сохранить изменения
	/// </summary>
	/// <param name="withSoftDelete">Использовать мягкое удаление</param>
	/// <param name="acceptAllChangesOnSuccess">Указывает, вызывается ли AcceptAllChanges() после успешной отправки изменений в базу данных</param>
	/// <param name="cancellationToken">Токен отмены</param>
	/// <returns>Количество записей состояния, записанных в базу данных</returns>
	Task<int> SaveChangesAsync(
		bool withSoftDelete = true,
		bool acceptAllChangesOnSuccess = true,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Сохранить изменения
	/// </summary>
	/// <param name="cancellationToken">Токен отмены</param>
	/// <returns>Количество записей состояния, записанных в базу данных</returns>
	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
