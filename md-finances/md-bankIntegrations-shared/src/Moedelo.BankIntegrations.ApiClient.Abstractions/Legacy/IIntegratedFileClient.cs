using System.Threading.Tasks;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using Moedelo.BankIntegrations.Models.Movement;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.Legacy
{
    public interface IIntegratedFileClient
    {
        Task<bool> TransferFilesToMdAsync(MDMovementList movementList, int firmId, IntegrationPartners integrationPartner, string requestId);
    }
}
