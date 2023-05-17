using System.Data;
using HostInitActions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio.AspNetCore;
using Npgsql;
using S3ApiTestTask.Application.Common.Configs;
using S3ApiTestTask.Application.Common.Extensions;
using S3ApiTestTask.Application.Common.Services;
using S3ApiTestTask.Infrastructure.InitExecutors;
using S3ApiTestTask.Infrastructure.Persistence;
using S3ApiTestTask.Infrastructure.Services;

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
		var s3Config = services.ConfigureAndGet<S3Config>(configuration, S3Config.ConfigSectionName);

		services.AddMinio(S3Service.MinioInternalClientName, x =>
		{
			x.Endpoint = s3Config.InternalUrl;
			x.AccessKey = s3Config.AccessKey;
			x.SecretKey = s3Config.SecterKey;
			x.Region = "eu-central-1";
		});

		services.AddMinio(S3Service.MinioExternalClientName, x =>
		{
			x.Endpoint = s3Config.ExternalUrl;
			x.AccessKey = s3Config.AccessKey;
			x.SecretKey = s3Config.SecterKey;
			x.Region = "eu-central-1";
		});

		return services.AddTransient<IS3Service, S3Service>();
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
