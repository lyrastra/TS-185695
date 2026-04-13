using System.Threading.Tasks;

using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;

namespace Moedelo.HomeV2.Client.Sberbank
{
    public interface ISberbankUserExistsApiClient : IDI
    {
        Task<SberbankUserExistsResponse> Get(string login);
    }
}