using MediatR;
using Microsoft.AspNetCore.Mvc;
using S3ApiTestTask.Application.Files.Commands.GenereateUploadLink;
using S3ApiTestTask.Application.Files.Commands.UploadFile;
using S3ApiTestTask.Application.Files.Queries.DownloadFile;
using S3ApiTestTask.Application.Files.Queries.GetFiles;
using S3ApiTestTask.Contracts.Requests.Files.GenereateUploadLink;
using S3ApiTestTask.Contracts.Requests.Files.GetFiles;
using S3ApiTestTask.Contracts.Requests.Files.UploadFile;

namespace S3ApiTestTask.Api.Controllers;

/// <summary>
/// Контроллер для Файлов
/// </summary>
[ApiController]
[Route("[controller]")]
public class AppFileController : ControllerBase
{
	private readonly IMediator _mediator;

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="mediator">Медиатор</param>
	public AppFileController(IMediator mediator)
		=> _mediator = mediator;

	/// <summary>
	/// Сгенерировать ссылку на загрузку файла
	/// </summary>
	/// <param name="cancellationToken">Токен отмены</param>
	/// <returns>Ссылка на загрузку файла</returns>
	[HttpPost("GenereateUploadLink")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<GenereateUploadLinkResponse> GenereateUploadLinkAsync(
		CancellationToken cancellationToken = default)
			=> await _mediator.Send(
				new GenereateUploadLinkCommand(),
				cancellationToken);

	/// <summary>
	/// Загрузить файл
	/// </summary>
	/// <param name="id">Идентификатор ссылки загрузки файла</param>
	/// <param name="file">Файл</param>
	/// <param name="cancellationToken">Токен отмены</param>
	/// <returns>Идентификатор файла</returns>
	[HttpPost("Upload/{id}")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<UploadFileResponse> UploadAsync(
		[FromRoute] Guid id,
		IFormFile file,
		CancellationToken cancellationToken)
			=> await _mediator.Send(
				new UploadFileCommand(id, file),
				cancellationToken);

	/// <summary>
	/// Скачать файл
	/// </summary>
	/// <param name="id">Идентификатор файла</param>
	/// <param name="cancellationToken">Токен отмены</param>
	/// <returns>Файл</returns>
	[HttpGet("Download/{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<FileStreamResult> DownloadAsync(
		[FromRoute] Guid id,
		CancellationToken cancellationToken)
			=> await _mediator.Send(new DownloadFileQuery(id), cancellationToken);

	/// <summary>
	/// Получить список загруженных файлов
	/// </summary>
	/// <param name="cancellationToken">Токен отмены</param>
	/// <returns>Файл</returns>
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<GetFilesResponse> GetAsync(CancellationToken cancellationToken)
			=> await _mediator.Send(new GetFilesQuery(), cancellationToken);
}
