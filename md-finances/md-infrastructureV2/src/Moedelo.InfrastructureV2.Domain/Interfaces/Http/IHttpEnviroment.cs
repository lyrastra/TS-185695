using System.Collections.Generic;
using System.Web;

namespace Moedelo.InfrastructureV2.Domain.Interfaces.Http;

public interface IHttpEnviroment
{
    bool HasHttpContext { get; }
    
    HttpContext CurrentContext { get; }

    Dictionary<string, object> ItemList { get; }

    /// <summary>
    /// Возвращает первый файл (или null) из контекста запроса
    /// </summary>
    HttpPostedFile GetFile();
}