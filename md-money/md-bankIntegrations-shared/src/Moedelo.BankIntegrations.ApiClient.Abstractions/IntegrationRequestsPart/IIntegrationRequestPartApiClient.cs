using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequests;
using Moedelo.BankIntegrations.Enums;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.IntegrationRequestsPart
{
    public interface IIntegrationRequestPartApiClient
    {
        Task<int> CreateNewAsync(
            int integrationRequestId,
            NewIntegrationRequestPartParamsDto requestDto,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyCollection<IntegrationRequestPartDto>> CreateMultipleAsync(
            int integrationRequestId,
            IReadOnlyCollection<NewIntegrationRequestPartParamsDto> requestDtos,
            CancellationToken cancellationToken = default);

        Task SetStatusAsync(
            int requestPartId,
            IntegrationRequestPartStatusEnum status,
            string logFile,
            CancellationToken cancellationToken = default);

        Task SetStatusWithVerificationAsync(
            int requestPartId,
            IntegrationRequestPartStatusEnum status,
            IntegrationRequestPartStatusEnum oldStatus,
            CancellationToken cancellationToken = default);

        Task SetExternalRequestIdAsync(
            int requestPartId,
            string externalRequestId,
            string logFile,
            CancellationToken cancellationToken = default);

        Task SetLogFileAsync(
            int integrationRequestId,
            int requestPartId,
            string logFile,
            CancellationToken cancellationToken = default);

        Task<IntegrationRequestPartDto> GetAsync(
            int integrationRequestId,
            int requestPartId,
            CancellationToken cancellationToken = default);

        Task<IntegrationRequestPartDto> GetByIdAsync(int requestPartId, CancellationToken cancellationToken = default);

        Task<IntegrationRequestPartDto> GetByExternalRequestIdAsync(
            IntegrationPartners integrationPartner,
            string externalRequestId,
            CancellationToken cancellationToken = default);
    }
}
