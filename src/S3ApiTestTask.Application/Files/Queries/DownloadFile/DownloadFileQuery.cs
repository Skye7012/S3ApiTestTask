using MediatR;
using S3ApiTestTask.Contracts.Requests.Files.DownloadFile;

namespace S3ApiTestTask.Application.Files.Queries.DownloadFile;

/// <summary>
/// Запрос на скачивание файла 
/// </summary>
public class DownloadFileQuery : IRequest<DownloadFileResponse>
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
