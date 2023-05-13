using MediatR;
using Microsoft.AspNetCore.Http;
using S3ApiTestTask.Contracts.Requests.Files.UploadFile;

namespace S3ApiTestTask.Application.Files.Commands.UploadFile;

/// <summary>
/// Команда для загрузки файла
/// </summary>
public class UploadFileCommand : IRequest<UploadFileResponse>
{
	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="file">Файл</param>
	/// <param name="id">Идентификатор</param>
	public UploadFileCommand(
		Guid id,
		IFormFile file)
	{
		Id = id;
		File = file;
	}

	/// <summary>
	/// Идентификатор
	/// </summary>
	public Guid Id { get; set; }

	/// <summary>
	/// Файл
	/// </summary>
	public IFormFile File { get; set; }
}
