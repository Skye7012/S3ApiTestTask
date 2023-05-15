using HostInitActions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using S3ApiTestTask.Application.Common.Configs;
using S3ApiTestTask.Application.Common.Services;

namespace S3ApiTestTask.Infrastructure.InitExecutors;

/// <summary>
/// Инициализатор базы данных
/// </summary>
public class S3InitExecutor : IAsyncInitActionExecutor
{
	private readonly IS3Service _s3Service;
	private readonly S3Config _s3Config;
	private readonly ILogger<S3InitExecutor> _logger;

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="s3Service">S3 хранилище</param>
	/// <param name="s3Config">Конфигурация S3</param>
	/// <param name="logger">Логгер</param>
	public S3InitExecutor(
		IS3Service s3Service,
		IOptions<S3Config> s3Config,
		ILogger<S3InitExecutor> logger)
	{
		_s3Service = s3Service;
		_s3Config = s3Config.Value;
		_logger = logger;
	}

	/// <summary>
	/// Провести инициализацию
	/// </summary>
	/// <param name="cancellationToken">Токен отмены</param>
	public async Task ExecuteAsync(CancellationToken cancellationToken)
	{
		if (await _s3Service.CreateBucketIfNotExist(_s3Config.BucketName, cancellationToken))
			_logger.LogInformation("Minio bucket существует");
		else
			_logger.LogInformation("Minio bucket был создан");
	}
}
