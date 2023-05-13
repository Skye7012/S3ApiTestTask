using MediatR;
using Microsoft.EntityFrameworkCore;
using S3ApiTestTask.Application.Common.Services;
using S3ApiTestTask.Contracts.Requests.Files.Common;
using S3ApiTestTask.Contracts.Requests.Files.GetFiles;

namespace S3ApiTestTask.Application.Files.Queries.GetFiles;

/// <summary>
/// Обработчик для of <see cref="GetFilesQuery"/>
/// </summary>
public class GetFilesQueryHandler : IRequestHandler<GetFilesQuery, GetFilesResponse>
{
	private readonly IApplicationDbContext _context;

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="context">Контекст БД</param>
	public GetFilesQueryHandler(IApplicationDbContext context)
		=> _context = context;

	/// <inheritdoc/>
	public async Task<GetFilesResponse> Handle(GetFilesQuery request, CancellationToken cancellationToken)
	{
		var files = await _context.Files
			.Select(x => new GetFileResponse
			{
				Id = x.Id,
				Name = x.Name,
				CreateOn = x.CreateOn,
				DeletedOn = x.DeletedOn,
			})
			.ToListAsync(cancellationToken);

		return new GetFilesResponse()
		{
			TotalCount = files.Count,
			Items = files,
		};
	}
}
