using System.Data;
using HostInitActions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio.AspNetCore;
using Npgsql;
using S3ApiTestTask.Application.Common.Configs;
using S3ApiTestTask.Application.Common.Services;
using S3ApiTestTask.Application.Common.Services.CacheRepositories;
using S3ApiTestTask.Infrastructure.InitExecutors;
using S3ApiTestTask.Infrastructure.Persistence;
using S3ApiTestTask.Infrastructure.Services;
using S3ApiTestTask.Infrastructure.Services.CacheRepositories;

namespace S3ApiTestTask.Infrastructure;

/// <summary>
/// Конфигуратор сервисов
/// </summary>
public static class InfrastructureServicesConfigurator
{
	/// <summary>
	/// Сконфигурировать сервисы
	/// </summary>
	/// <param name="services">Билдер приложения</param>
	/// <param name="configuration">Конфигурации приложения</param>
	public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		=> services
			.AddDatabase(configuration)
			.AddS3Storage(configuration)
			.AddRedis(configuration)
			.AddInitExecutors();

	/// <summary>
	/// Сконфигурировать подключение к БД
	/// </summary>
	/// <param name="services">Сервисы</param>
	/// <param name="configuration">Конфигурации приложения</param>
	private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
	{
		var connString = configuration.GetConnectionString("Db")!;
		services.AddDbContext<ApplicationDbContext>(opt =>
			{
				opt.UseNpgsql(connString);
				opt.UseSnakeCaseNamingConvention();
			});

		services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

		services.AddTransient<IDbConnection>(_ =>
		{
			var dbConnection = new NpgsqlConnection(connString);
			dbConnection.Open();
			return dbConnection;
		});

		return services;
	}

	/// <summary>
	/// Сконфигурировать хранилище S3
	/// </summary>
	/// <param name="services">Сервисы</param>
	/// <param name="configuration">Конфигурации приложения</param>
	private static IServiceCollection AddS3Storage(this IServiceCollection services, IConfiguration configuration)
	{
		var connString = new Uri(configuration.GetConnectionString("S3")!);
		services.AddMinio(connString);

		services.Configure<FilesConfig>(configuration.GetSection(FilesConfig.ConfigSectionName));

		services.AddTransient<IS3Service, S3Service>();

		return services.AddTransient<IFileCacheRepository, FileCacheRepository>();
	}

	/// <summary>
	/// Сконфигурировать Redis
	/// </summary>
	/// <param name="services">Сервисы</param>
	/// <param name="configuration">Конфигурации приложения</param>
	private static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
	{
		var connString = configuration.GetConnectionString("Redis")!;
		return services.AddStackExchangeRedisCache(opt => opt.Configuration = connString);
	}

	/// <summary>
	/// Сконфигурировать инициализаторы сервисов
	/// </summary>
	/// <param name="services">Сервисы</param>
	private static IServiceCollection AddInitExecutors(this IServiceCollection services)
	{
		services.AddAsyncServiceInitialization()
			.AddInitActionExecutor<DbInitExecutor>()
			.AddInitActionExecutor<S3InitExecutor>();

		return services;
	}
}
