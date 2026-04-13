using Moedelo.BankIntegrations.ApiClient.Dto.IntegratedUser;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.Legacy
{
    public interface IIntegrationSsoApiClient
    {
        Task SaveFromSso(SaveFromSsoDto request, HttpQuerySetting setting = null);
    }
}
