using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequests;
using Moedelo.BankIntegrations.Enums;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.IntegrationRequestParts
{
    public interface IIntegrationRequestPartApiClient
    {
        Task<int> CreateNewAsync(int integrationRequestId, NewIntegrationRequestPartParamsDto paramsDto);
        Task UpdateAsync(int integrationRequestId, int requestPartId,
            UpdateIntegrationRequestPartParamsDto paramsDto);
        Task SetLogFileIdAsync(int integrationRequestId, int requestPartId, string logFileId);
        Task SetLogFileAsync(int integrationRequestId, int requestPartId, string logFile);
        Task<IntegrationRequestPartDto> GetAsync(int integrationRequestId, int requestPartId);
        Task<IReadOnlyCollection<IntegrationRequestPartDto>> GetPartsOfRequestAsync(int integrationRequestId);
        Task<IntegrationRequestPartDto> GetByExternalRequestIdAsync(IntegrationPartners integrationPartner, string externalRequestId);
        Task SetStatusForAllPartsOfIntegrationRequestsAsync(IReadOnlyCollection<int> integrationRequestIds, IntegrationRequestPartStatusEnum newStatus);
    }
}
