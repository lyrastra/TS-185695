using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.ApiClient.Abstractions.IntegrationRequestsPart;
using Moedelo.BankIntegrations.ApiClient.Dto;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequests;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequestsPart;
using Moedelo.BankIntegrations.Enums;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.BankIntegrations.ApiClient.IntegrationRequestsPart
{
    [InjectAsSingleton(typeof(IIntegrationRequestPartApiClient))]
    public class IntegrationRequestPartApiClient : BaseApiClient, IIntegrationRequestPartApiClient
    {
        private const string IntegrationRequestPartController = "/private/api/v1/integration/request";

        public IntegrationRequestPartApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<IntegrationRequestPartApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("IntegrationApiNetCore"),
                logger)
        {
        }

        public Task<int> CreateNewAsync(
            int integrationRequestId,
            NewIntegrationRequestPartParamsDto requestDto,
            CancellationToken cancellationToken = default)
        {
            var url = $"{IntegrationRequestPartController}/{integrationRequestId}/part";

            return PostAsync<NewIntegrationRequestPartParamsDto, int>(
                url,
                requestDto,
                cancellationToken: cancellationToken);
        }

        public async Task<IReadOnlyCollection<IntegrationRequestPartDto>> CreateMultipleAsync(
            int integrationRequestId,
            IReadOnlyCollection<NewIntegrationRequestPartParamsDto> requestDtos,
            CancellationToken cancellationToken = default)
        {
            var url = $"{IntegrationRequestPartController}/{integrationRequestId}/part/CreateMultiple";
            var body = requestDtos?.ToArray() ?? Array.Empty<NewIntegrationRequestPartParamsDto>();

            var result = await PostAsync<NewIntegrationRequestPartParamsDto[], IntegrationRequestPartDto[]>(
                url,
                body,
                cancellationToken: cancellationToken);

            return result;
        }

        public Task SetStatusAsync(
            int requestPartId,
            IntegrationRequestPartStatusEnum status,
            string logFile,
            CancellationToken cancellationToken = default)
        {
            var url = $"{IntegrationRequestPartController}/part/{requestPartId}/SetStatusAsync";
            var body = new SetIntegrationRequestPartStatusRequestDto
            {
                Status = status,
                LogFile = logFile
            };

            return PutAsync(url, body, cancellationToken: cancellationToken);
        }

        public Task SetStatusWithVerificationAsync(
            int requestPartId,
            IntegrationRequestPartStatusEnum status,
            IntegrationRequestPartStatusEnum oldStatus,
            CancellationToken cancellationToken = default)
        {
            var url = $"{IntegrationRequestPartController}/part/{requestPartId}/SetStatusWithVerification";
            var body = new SetIntegrationRequestPartStatusWithVerificationRequestDto
            {
                Status = status,
                OldStatus = oldStatus
            };

            return PutAsync(url, body, cancellationToken: cancellationToken);
        }

        public Task SetExternalRequestIdAsync(
            int requestPartId,
            string externalRequestId,
            string logFile,
            CancellationToken cancellationToken = default)
        {
            var url = $"{IntegrationRequestPartController}/part/{requestPartId}/SetExternalRequestId";
            var body = new SetIntegrationRequestPartExternalRequestDto
            {
                ExternalRequestId = externalRequestId,
                LogFile = logFile
            };

            return PutAsync(url, body, cancellationToken: cancellationToken);
        }

        public Task SetLogFileAsync(
            int integrationRequestId,
            int requestPartId,
            string logFile,
            CancellationToken cancellationToken = default)
        {
            var url = $"{IntegrationRequestPartController}/{integrationRequestId}/part/{requestPartId}/logFile";

            return PutAsync(url, new ValueDto<string>(logFile), cancellationToken: cancellationToken);
        }

        public Task<IntegrationRequestPartDto> GetAsync(
            int integrationRequestId,
            int requestPartId,
            CancellationToken cancellationToken = default)
        {
            var url = $"{IntegrationRequestPartController}/{integrationRequestId}/part/{requestPartId}";

            return GetAsync<IntegrationRequestPartDto>(url, cancellationToken: cancellationToken);
        }

        public Task<IntegrationRequestPartDto> GetByIdAsync(
            int requestPartId,
            CancellationToken cancellationToken = default)
        {
            var url = $"{IntegrationRequestPartController}/part/GetById";
            var queryParams = new { partId = requestPartId };

            return GetAsync<IntegrationRequestPartDto>(url, queryParams, cancellationToken: cancellationToken);
        }

        public Task<IntegrationRequestPartDto> GetByExternalRequestIdAsync(
            IntegrationPartners integrationPartner,
            string externalRequestId,
            CancellationToken cancellationToken = default)
        {
            var url = $"{IntegrationRequestPartController}/part/find-one";
            var queryParams = new { integrationPartner, externalRequestId };

            return GetAsync<IntegrationRequestPartDto>(url, queryParams, cancellationToken: cancellationToken);
        }
    }
}
