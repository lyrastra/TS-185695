using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using IntegrationPartners = Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums.IntegrationPartners;

namespace Moedelo.BankIntegrationsV2.Client.Validation
{
    public interface ISettlementAccountValidationClient : IDI
    {
        Task<bool> ValidateNumber(string settlementNumber, IntegrationPartners integrationPartners, int firmId);
    }
}