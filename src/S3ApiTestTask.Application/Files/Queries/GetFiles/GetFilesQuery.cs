using MediatR;
using S3ApiTestTask.Contracts.Requests.Files.GetFiles;

namespace S3ApiTestTask.Application.Files.Queries.GetFiles;

/// <summary>
/// Запрос на получение списка загруженных файлов
/// </summary>
public class GetFilesQuery : IRequest<GetFilesResponse>
{
}
