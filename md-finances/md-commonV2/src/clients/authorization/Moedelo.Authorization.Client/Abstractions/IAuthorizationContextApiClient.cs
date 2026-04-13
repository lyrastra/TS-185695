using System.Threading.Tasks;
using Moedelo.Authorization.Dto;

namespace Moedelo.Authorization.Client.Abstractions;

public interface IAuthorizationContextApiClient
{
    Task<AuthorizationContextDto> GetAsync(int firmId, int userId);
}