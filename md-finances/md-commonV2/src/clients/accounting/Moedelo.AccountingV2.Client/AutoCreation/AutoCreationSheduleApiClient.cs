using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.AutoCreation;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.AutoCreation
{
    [InjectAsSingleton]
    public class AutoCreationSheduleApiClient : BaseApiClient, IAutoCreationSheduleApiClient
    {
        private readonly SettingValue apiEndPoint;
        private readonly HttpQuerySetting runAutoCreationHttpSetting = new HttpQuerySetting(TimeSpan.FromMinutes(5));

        public AutoCreationSheduleApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AccountingApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        private class LegacyDataDto<T>
        {
            public T Data { get; set; }
        }

        public async Task<List<int>> GetFirmsWithActiveSchedulesAsync(DateTime date)
        {
            var url = "/AutoCreationScheduleApi/GetFirmsWithActiveSchedules";
            var response = await GetAsync<LegacyDataDto<List<int>>>(url, new { date }).ConfigureAwait(false);

            return response.Data;
        }

        public async Task<PrimaryDocumentAutoCreationInfoDto> GetDocumentAutoCreationInfo(int firmId, int userId, long documentBaseId)
        {
            var uri = $"/PrimaryDocumentsApi/GetDocumentAutoCreationInfo?firmId={firmId}&userId={userId}&documentBaseId={documentBaseId}";
            var response = await GetAsync<LegacyDataDto<PrimaryDocumentAutoCreationInfoDto>>(uri).ConfigureAwait(false);

            return response.Data;
        }

        public async Task<List<long>> RunAutoCreationSchedulesAsync(int firmId, int userId, DateTime untilDate)
        {
            var url = $"/PrimaryDocumentsApi/RunAutoCreationSchedules?firmId={firmId}&userId={userId}&untilDate={untilDate:yyyy-MM-dd}";

            var response = await PostAsync<LegacyDataDto<List<long>>>(
                url, 
                setting: runAutoCreationHttpSetting).ConfigureAwait(false);

            return response.Data;
        }

        public async Task DeleteScheduleForDocumentAsync(int firmId, int userId, long documentBaseId)
        {
            await DeleteAsync(uri: $"/PrimaryDocumentsApi/Delete?firmId={firmId}&userId={userId}&documentBaseId={documentBaseId}")
                .ConfigureAwait(false);
        }
    }
}