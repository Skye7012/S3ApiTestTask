using Microsoft.EntityFrameworkCore;

namespace S3ApiTestTask.Application.Common.Services;

/// <summary>
/// Интерфейс контекста БД
/// </summary>
public interface IDbContext : IDisposable
{
	/// <summary>
	/// Экземпляр текущего контекста БД
	/// </summary>
	DbContext Instance { get; }
}
