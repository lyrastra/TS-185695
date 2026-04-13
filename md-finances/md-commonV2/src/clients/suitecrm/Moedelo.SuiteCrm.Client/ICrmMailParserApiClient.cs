using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.SuiteCrm.Client
{
    public interface ICrmMailParserApiClient : IDI
    {
        Task<bool> ParseMailAsync(string email);

        Task<bool> ParseMarketingMailAsync(string email);
    }
}