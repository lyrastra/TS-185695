using System;
using System.Collections.Generic;
using System.Web;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Http;

namespace Moedelo.InfrastructureV2.Domain.Models.Http;

[InjectPerWebRequest(typeof(IHttpEnviroment))]
public sealed class HttpEnvironment : IHttpEnviroment
{
    public Dictionary<string, object> ItemList { get; } = new();
    private readonly HttpContext httpContextRef = HttpContext.Current;

    public bool HasHttpContext => httpContextRef is not null;

    public HttpContext CurrentContext => httpContextRef
                                         ?? throw new ArgumentNullException(
                                             nameof(CurrentContext),
                                             $"При инициализации объекта типа {nameof(HttpEnvironment)} значение HttpContext.Current было равно null");

    public HttpPostedFile GetFile()
    {
        var fileList = CurrentContext.Request.Files;

        return fileList.Count == 0 ? null : fileList.Get(0);
    }
}
