using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.OnlineTv;
using Moedelo.HomeV2.Dto.OnlineTv;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.HomeV2.Client.OnlineTv
{
    [InjectAsSingleton]
    public class OnlineTvClient : BaseApiClient, IOnlineTvClient
    {
        private readonly SettingValue apiEndPoint;

        public OnlineTvClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("HomePrivateApiEndpoint") ;
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/rest/onlineTv/";
        }

        public Task<List<OnlineTvArchiveDto>> GetOnlineTvArchive(int count)
        {
            return GetAsync<List<OnlineTvArchiveDto>>("V2/archive", new { count });
        }

        public Task<string> GetArchiveVideoByEventId(int id)
        {
            return GetAsync<string>("V2/GetArchiveVideoByEventId", new { id });
        }

        public Task<OnlineTvArchiveDto> GetArchiveEventById(int id)
        {
            return GetAsync<OnlineTvArchiveDto>("V2/GetArchiveEventById", new { id });
        }

        public Task<List<OnlineTvArchiveDto>> GetArchivesByType(OnlineTvCategoryType type)
        {
            return GetAsync<List<OnlineTvArchiveDto>>("V2/GetArchivesByType", new { type });
        }

        public Task<List<WebinarCategoryDto>> GetWebinarCategoriesByType(OnlineTvCategoryType type)
        {
            return GetAsync<List<WebinarCategoryDto>>("V2/GetWebinarCategoriesByType", new { type });
        }

        public Task<OnlineTvEventResponseDto> GetEventByIdAsync(int id)
        {
            return GetAsync<OnlineTvEventResponseDto>("V2/GetEventByIdAsync", new { id });
        }

        public Task<List<OnlineTvEventResponseDto>> GetEventsByListIdsAsync(List<int> ids)
        {
            return GetAsync<List<OnlineTvEventResponseDto>>("V2/GetEventsByListIdsAsync?" + CreateQueryFromListIds(ids));
        }

        public Task<List<OnlineTvEventResponseDto>> GetEventsByTimeIntervalAsync(
            DateTime startDateTime,
            DateTime endDateTime)
        {
            string startDate = startDateTime.ToString("yyyyMMddHHmmss");
            string endDate = endDateTime.ToString("yyyyMMddHHmmss");
            
            return GetAsync<List<OnlineTvEventResponseDto>>("V2/GetEventsByTimeIntervalAsync", new { startDate, endDate });
        }

        public Task<List<OnlineTvRegisteredPeopleResponseDto>> GetRegistredPeopleByEventIdAsync(int id)
        {
            return GetAsync<List<OnlineTvRegisteredPeopleResponseDto>>("V2/GetRegistredPeopleByEventIdAsync", new { id });
        }

        public async Task<OnlineTvArchiveDto> GetArchiveEventByIdAsync(int id)
        {
            var response = await GetAsync<DataRequestWrapper<OnlineTvArchiveDto>>("/GetArchiveEventById", new { id }).ConfigureAwait(false);
            return response.Data;
        }

        public async Task<List<OnlineTvArchiveDto>> GetArchivesByTypeAsync(OnlineTvCategoryType type)
        {
            var response = await GetAsync<ListWrapper<OnlineTvArchiveDto>>("GetArchivesByType", new { type }).ConfigureAwait(false);
            return response.Items;
        }

        public async Task<string> GetArchiveVideoByEventIdAsync(int id)
        {
            var response = await GetAsync<DataRequestWrapper<string>>("GetArchiveVideoByEventId", new { id }).ConfigureAwait(false);
            return response.Data;
        }

        public async Task<List<WebinarCategoryDto>> GetWebinarCategoriesByTypeAsync(OnlineTvCategoryType type)
        {
            var response = await GetAsync<ListWrapper<WebinarCategoryDto>>("GetWebinarCategoriesByType", new { type }).ConfigureAwait(false);
            return response.Items;
        }

        private string CreateQueryFromListIds(List<int> ids)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var id in ids)
            {
                sb.AppendFormat("&ids={0}", id);
            }
            return sb.ToString().Substring(1);
        }
    }
}