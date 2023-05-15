using System.Net.Mime;
using Microsoft.Extensions.Options;
using Minio;
using Minio.AspNetCore;
using S3ApiTestTask.Application.Common.Configs;
using S3ApiTestTask.Application.Common.Services;
using S3ApiTestTask.Domain.Entities;

namespace S3ApiTestTask.Infrastructure.Services;

/// <summary>
/// Сервис для работы с S3 хранилищем
/// </summary>
public class S3Service : IS3Service
{
	public const string MinioInternalClientName = "Internal";
	public const string MinioExternalClientName = "External";

	private readonly MinioClient _internalClient;
	private readonly MinioClient _externalClient;
	private readonly IDateTimeProvider _dateTimeProvider;
	private readonly S3Config _config;

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="s3Config">Конфигурация S3</param>
	/// <param name="dateTimeProvider">Провайдер даты и времени</param>
	/// <param name="minioClientFactory">Фабрика minio клиентов</param>
	public S3Service(
		IOptions<S3Config> s3Config,
		IDateTimeProvider dateTimeProvider,
		IMinioClientFactory minioClientFactory)
	{
		_dateTimeProvider = dateTimeProvider;
		_config = s3Config.Value;

		_internalClient = minioClientFactory.CreateClient(MinioInternalClientName);
		_externalClient = minioClientFactory.CreateClient(MinioExternalClientName);
	}

	/// <inheritdoc/>
	public async Task<bool> IsObjectExistsAsync(string fileName, CancellationToken cancellationToken)
	{
		try
		{
			var args = new StatObjectArgs()
				.WithBucket(_config.BucketName)
				.WithObject(fileName);

			await _internalClient.StatObjectAsync(args, cancellationToken);

			return true;
		}
		catch
		{
			return false;
		}
	}

	/// <inheritdoc/>
	public bool IsFileUploadingTimeExpired(AppFile file)
		=> file.CreateOn.AddSeconds(_config.PresignedUrlLifetime) < _dateTimeProvider.UtcNow;

	/// <inheritdoc/>
	public async Task<string> PresignedPutObjectAsync(string fileName)
	{
		var contentDisposition = new ContentDisposition
		{
			FileName = fileName,
			Inline = false,
		};

		var args = new PresignedPutObjectArgs()
			.WithBucket(_config.BucketName)
			.WithObject(fileName)
			.WithExpiry(_config.PresignedUrlLifetime)
			.WithHeaders(new Dictionary<string, string>
			{
				["Content-Disposition"] = contentDisposition.ToString(),
				["Content-Type"] = MediaTypeNames.Application.Octet,
				["content-type"] = MediaTypeNames.Application.Octet,
			});

		return await _externalClient.PresignedPutObjectAsync(args);
	}

	/// <inheritdoc/>
	public async Task<string> PresignedGetObjectAsync(string fileName)
	{
		var args = new PresignedGetObjectArgs()
			.WithBucket(_config.BucketName)
			.WithObject(fileName)
			.WithExpiry(_config.PresignedUrlLifetime);

		return await _externalClient.PresignedGetObjectAsync(args);
	}

	/// <inheritdoc/>
	public async Task<bool> CreateBucketIfNotExist(
		string bucketName,
		CancellationToken cancellationToken)
	{
		var isBucketCreated = await _internalClient.BucketExistsAsync(
			new BucketExistsArgs()
				.WithBucket(_config.BucketName),
			cancellationToken);

		if (!isBucketCreated)
			await _internalClient.MakeBucketAsync(
				new MakeBucketArgs()
					.WithBucket(_config.BucketName),
				cancellationToken);

		return isBucketCreated;
	}
}
