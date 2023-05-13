using MediatR;
using S3ApiTestTask.Contracts.Requests.Files.GenereateUploadLink;

namespace S3ApiTestTask.Application.Files.Commands.GenereateUploadLink;

/// <summary>
/// Команда на генерацию ссылки на загрузку файла
/// </summary>
public class GenereateUploadLinkCommand : IRequest<GenereateUploadLinkResponse>
{
}
