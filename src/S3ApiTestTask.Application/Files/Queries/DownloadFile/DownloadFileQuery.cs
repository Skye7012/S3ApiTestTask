using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace S3ApiTestTask.Application.Files.Queries.DownloadFile;

/// <summary>
/// Запрос на скачивание файла 
/// </summary>
public class DownloadFileQuery : IRequest<FileStreamResult>
{
	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="id">Идентификатор</param>
	public DownloadFileQuery(Guid id)
		=> Id = id;

	/// <summary>
	/// Идентификатор файла
	/// </summary>
	public Guid Id { get; set; }
}
