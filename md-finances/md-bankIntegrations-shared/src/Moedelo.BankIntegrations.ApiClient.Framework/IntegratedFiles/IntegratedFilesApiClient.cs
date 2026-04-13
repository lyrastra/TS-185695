using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegratedFile;
using Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.IntegratedFiles;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BankIntegrations.ApiClient.Framework.IntegratedFiles
{
    [InjectAsSingleton(typeof(IIntegratedFilesApiClient))]
    internal sealed class IntegratedFilesApiClient : BaseCoreApiClient, IIntegratedFilesApiClient
    {
        private readonly SettingValue endpoint;

        public IntegratedFilesApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator,
                responseParser,
                settingRepository,
                auditTracer,
                auditScopeManager)
        {
            this.endpoint = settingRepository.Get("IntegrationApiNetCore");
        }

        protected override string GetApiEndpoint()
        {
            return endpoint.Value;
        }

        public async Task<IntegratedFileDto> GetAsync(int fileId, int firmId)
        {
            return await GetAsync<IntegratedFileDto>(
                    $"/private/api/v1/IntegratedFiles/{fileId}?firmId={firmId}",
                    queryHeaders: await GetUnidentifiedTokenHeaders().ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<IntegratedFileDto[]> GetNotProcessedAsync(int firmId)
        {
            return await GetAsync<IntegratedFileDto[]>(
                    $"/private/api/v1/IntegratedFiles/actual?firmId={firmId}",
                    queryHeaders: await GetUnidentifiedTokenHeaders().ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<IntegratedFileDto[]> GetNotProcessedAsync(int firmId, IntegrationPartners integratorId)
        {
            return await GetAsync<IntegratedFileDto[]>(
                    $"/private/api/v1/IntegratedFiles/actual?firmId={firmId}&integratorId={(int)integratorId}",
                    queryHeaders: await GetUnidentifiedTokenHeaders().ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<int> CountNotProcessedNotEmptyAsync(int firmId)
        {
            return await GetAsync<int>(
                    $"/private/api/v1/IntegratedFiles/actual/count?firmId={firmId}",
                    queryHeaders: await GetUnidentifiedTokenHeaders().ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<IReadOnlyCollection<int>> SaveAsync(
            IReadOnlyCollection<IntegratedFileCreationRequestDto> files)
        {
            return await PostAsync<IReadOnlyCollection<IntegratedFileCreationRequestDto>, int[]>(
                    $"/private/api/v1/IntegratedFiles/multiple",
                    files,
                    queryHeaders: await GetUnidentifiedTokenHeaders().ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<IntegratedFileDto> GetLastNotProcessedAsync(int firmId, IntegrationPartners integratorId)
        {
            return await GetAsync<IntegratedFileDto>(
                    $"/private/api/v1/IntegratedFiles/actual/last?firmId={firmId}&integratorId={(int)integratorId}",
                    queryHeaders: await GetUnidentifiedTokenHeaders().ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task UpdateFileContentAsync(int fileId, int firmId, string fileText)
        {
            await PutAsync(
                    $"/private/api/v1/IntegratedFiles/{fileId}/content?firmId={firmId}",
                    // оборачиваем строку в dto, чтобы не делать кастомный json-десериализатор на серверной стороне
                    new IntegratedFileContentDto
                    {
                        FileText = fileText
                    },
                    queryHeaders: await GetUnidentifiedTokenHeaders().ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task SetIsAddedAsync(int fileId, int firmId)
        {
            await PutAsync(
                    $"/private/api/v1/IntegratedFiles/{fileId}/added?firmId={firmId}",
                    new { value = true }, // содержимое тела игнорируется
                    queryHeaders: await GetUnidentifiedTokenHeaders().ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task SetIsSkippedAsync(int fileId, int firmId)
        {
            await PutAsync(
                    $"/private/api/v1/IntegratedFiles/{fileId}/skipped?firmId={firmId}",
                    new { value = true }, // содержимое тела игнорируется
                    queryHeaders: await GetUnidentifiedTokenHeaders().ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<int> CountDailyFilesWithEmptyIntegrationRequestIdAsync(DateTime date)
        {
            return await GetAsync<int>(
                    $"/private/api/v1/IntegratedFiles/CountDailyFilesWithEmptyIntegrationRequestId",
                    new { date = date.Date },
                    queryHeaders: await GetUnidentifiedTokenHeaders().ConfigureAwait(false),
                    new HttpQuerySetting(){ Timeout = TimeSpan.FromMinutes(1)})
                .ConfigureAwait(false);
        }
    }
}
