using System.Threading.Tasks;
using Moedelo.BankIntegrations.Enums;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.Utils.Framework.Abstractions.Services
{
    public interface IIntegrationMenuService
    {
        Task SetAsync(int firmId, IntegrationPartners partner, IntegrationSource integrationSource);
        Task<IntegrationSource> GetAsync(int firmId, IntegrationPartners partner);
    }
}