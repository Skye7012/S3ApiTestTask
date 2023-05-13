using Microsoft.EntityFrameworkCore;
using S3ApiTestTask.Application.Common.Services;
using S3ApiTestTask.Domain.Entities;
using S3ApiTestTask.Domain.Entities.Common;

namespace S3ApiTestTask.Infrastructure.Persistence;

/// <summary>
/// Контекст БД данного приложения
/// </summary>
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
	private readonly IDateTimeProvider _dateTimeProvider;

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="options">Опции контекста</param>
	/// <param name="dateTimeProvider">Провайдер даты и времени</param>
	public ApplicationDbContext(
		DbContextOptions<ApplicationDbContext> options,
		IDateTimeProvider dateTimeProvider)
		: base(options)
			=> _dateTimeProvider = dateTimeProvider;

	/// <inheritdoc/>
	public DbContext Instance => this;

	/// <summary>
	/// Файлы
	/// </summary>
	public DbSet<AppFile> Files { get; private set; } = default!;

	/// <inheritdoc/>
	protected override void OnModelCreating(ModelBuilder modelBuilder)
		=> modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

	/// <inheritdoc/>
	public int SaveChanges(bool withSoftDelete = true, bool acceptAllChangesOnSuccess = true)
	{
		HandleSaveChangesLogic(withSoftDelete);

		return base.SaveChanges(acceptAllChangesOnSuccess);
	}

	/// <inheritdoc/>
	public async Task<int> SaveChangesAsync(
		bool withSoftDelete = true,
		bool acceptAllChangesOnSuccess = true,
		CancellationToken cancellationToken = default)
	{
		HandleSaveChangesLogic(withSoftDelete);

		return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
	}

	/// <summary>
	/// Сохранить изменения
	/// </summary>
	/// <param name="cancellationToken">Токен отмены</param>
	/// <returns>Количество записей состояния, записанных в базу данных</returns>
	public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		HandleSaveChangesLogic(true);

		return await base.SaveChangesAsync(cancellationToken);
	}

	/// <summary>
	/// Обработать логику сохранения изменений
	/// </summary>
	/// <param name="withSoftDelete">Использовать мягкое удаление</param>
	private void HandleSaveChangesLogic(bool withSoftDelete)
	{
		HandleEntityBaseCreatedMetadata();

		if (!withSoftDelete)
			return;

		HandleSoftDelete();
	}

	/// <summary>
	/// Обработать метаданные добавления у базовых сущностей
	/// </summary>
	private void HandleEntityBaseCreatedMetadata()
	{
		var changeSet = ChangeTracker.Entries<EntityBase>();

		if (changeSet?.Any() != true)
			return;

		foreach (var entry in changeSet.Where(c => c.State == EntityState.Added))
		{
			entry.Entity.CreateOn = _dateTimeProvider.UtcNow;
		}
	}

	/// <summary>
	/// Обработать мягкое удаления сущностей
	/// </summary>
	private void HandleSoftDelete()
	{
		var deleteChangeSet = ChangeTracker.Entries<ISoftDeletable>();

		if (deleteChangeSet?.Any() == true)
		{
			foreach (var entry in deleteChangeSet.Where(c => c.State == EntityState.Deleted))
			{
				entry.Entity.DeletedOn = _dateTimeProvider.UtcNow;
				entry.State = EntityState.Modified;
			}
		}
	}
}
