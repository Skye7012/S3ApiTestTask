//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Infrastructure;
//using Microsoft.AspNetCore.Mvc.Routing;
//using S3ApiTestTask.Application.Common.Interfaces;

//namespace S3ApiTestTask.Infrastructure.Services;

///// <inheritdoc/>
//public class LinkGenerator : ILinkGenerator
//{
//	private readonly IUrlHelper _urlHelper;
//	private readonly IActionContextAccessor _contextAccessor;

//	/// <summary>
//	/// Конструктор
//	/// </summary>
//	/// <param name="urlHelper">Билдер url</param>
//	/// <param name="contextAccessor">Контекст запроса</param>
//	public LinkGenerator(IUrlHelper urlHelper, IActionContextAccessor contextAccessor)
//	{
//		_urlHelper = urlHelper;
//		_contextAccessor = contextAccessor;
//	}

//	/// <summary>
//	/// Загрузить файл
//	/// </summary>
//	/// <param name="stream">Файл</param>
//	/// <param name="fileName">Наименование файла</param>
//	/// <param name="cancellationToken">Токен отмены</param>
//	/// <returns></returns>
//	string GetUploadUrl(
//		Stream stream,
//		string fileName,
//		CancellationToken cancellationToken)
//	{
//		var urlHelper = new UrlHelper(_contextAccessor.ActionContext);
//		urlHelper.Action(nameof(AppFileController))
//	}
//}
