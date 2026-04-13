using System.Net;

namespace Moedelo.InfrastructureV2.WebApi.Validation.Http
{
    public static class HttpStatusCodeEx
    {
        public const HttpStatusCode ValidationFailed = (HttpStatusCode)422;
    }
}