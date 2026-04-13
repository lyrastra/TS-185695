using Microsoft.AspNetCore.Http;

namespace Moedelo.Common.ExecutionContext.Auth.Authentication.Extensions
{
    internal static class HttpRequestExtensions
    {
        public static int? GetCompanyIdParam(this HttpRequest request)
        {
            const string companyIdParamName = AuthenticationConstants.CompanyIdParamName;
            return request.Query[companyIdParamName].Count > 0 &&
                int.TryParse(request.Query[companyIdParamName][0], out var companyId)
                    ? (int?)companyId
                    : null;
        }
    }
}
