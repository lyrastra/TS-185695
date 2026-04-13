using System.Threading.Tasks;

namespace Moedelo.Common.AccessRules.External.Authorization
{
    internal interface IAuthorizationContextApiClient
    {
        Task<AuthorizationContextDto> GetAsync(int firmId, int userId);
    }
}