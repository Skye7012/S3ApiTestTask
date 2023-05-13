using S3ApiTestTask.Contracts.Requests.Common;
using S3ApiTestTask.Contracts.Requests.Files.Common;

namespace S3ApiTestTask.Contracts.Requests.Files.GetFiles;

/// <summary>
/// Ответ на запрос получения списка загруженных файлов
/// </summary>
public class GetFilesResponse : BaseGetResponse<GetFileResponse>
{
}
