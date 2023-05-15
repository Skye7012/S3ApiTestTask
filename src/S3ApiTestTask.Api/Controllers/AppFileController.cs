using MediatR;
using Microsoft.AspNetCore.Mvc;
using S3ApiTestTask.Application.Files.Commands.GenereateUploadLink;
using S3ApiTestTask.Application.Files.Queries.DownloadFile;
using S3ApiTestTask.Application.Files.Queries.GetFiles;
using S3ApiTestTask.Contracts.Requests.Files.GenereateUploadLink;
using S3ApiTestTask.Contracts.Requests.Files.GetFiles;

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
	/// <param name="request">Запрос</param>
	/// <param name="cancellationToken">Токен отмены</param>
	/// <returns>Ссылка на загрузку файла</returns>
	[HttpPost("GenereateUploadLink")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<GenereateUploadLinkResponse> GenereateUploadLinkAsync(
		GenereateUploadLinkRequest request,
		CancellationToken cancellationToken = default)
			=> await _mediator.Send(
				new GenereateUploadLinkCommand
				{
					FileName = request.FileName,
				},
				cancellationToken);

	/// <summary>
	/// Получить список загруженных файлов
	/// </summary>
	/// <param name="cancellationToken">Токен отмены</param>
	/// <returns>Файл</returns>
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<GetFilesResponse> GetAsync(CancellationToken cancellationToken)
			=> await _mediator.Send(new GetFilesQuery(), cancellationToken);

	/// <summary>
	/// Скачать файл
	/// </summary>
	/// <param name="id">Идентификатор файла</param>
	/// <param name="cancellationToken">Токен отмены</param>
	/// <returns>Файл</returns>
	[HttpGet("Download/{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> DownloadAsync(
		[FromRoute] Guid id,
		CancellationToken cancellationToken)
	{
		var response = await _mediator.Send(new DownloadFileQuery(id), cancellationToken);

		return Redirect(response.DownloadLink);
	}
}
