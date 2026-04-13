using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.TagsV2.Client.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;

namespace Moedelo.TagsV2.Client.Clients
{
    [InjectAsSingleton]
    public class TagsApiClient : BaseApiClient, ITagsApiClient
    {
        private readonly SettingValue apiEndpoint;

        public TagsApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("TagsApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public async Task<TagDto> GetByNameAsync(int firmId, int userId, string name)
        {
            var response = await GetAsync<DataResponseWrapper<TagDto>>($"/GetByName?firmId={firmId}&userId={userId}&name={name}").ConfigureAwait(false);
            return response.Data;
        }

        public async Task<List<TagDto>> GetByNamesAsync(int firmId, int userId, IReadOnlyCollection<string> names)
        {
            var response = await PostAsync<IReadOnlyCollection<string>, ListResponseWrapper<TagDto>>($"/GetByNames?firmId={firmId}&userId={userId}", names).ConfigureAwait(false);
            return response.Items;
        }

        public async Task<List<TagDto>> GetByEntitiesAsync(int firmId, int userId, EntityListRequestDto dto)
        {
            var response = await PostAsync<EntityListRequestDto, ListResponseWrapper<TagDto>>($"/GetByEntities?firmId={firmId}&userId={userId}", dto).ConfigureAwait(false);
            return response.Items;
        }

        public async Task<List<TagDto>> GetForUnionKontragentsAsync(int firmId, int userId, GetForUnionKontragentsRequestDto dto)
        {
            var request = new
            {
                KontragentId = dto.BaseKontragentId,
                KontragentIds = dto.MergedKontragentIds
            };
            var response = await PostAsync<object, ListResponseWrapper<TagDto>>($"/GetForUnionKontragents?firmId={firmId}&userId={userId}", request).ConfigureAwait(false);
            return response.Items;
        }

        public async Task<long> SaveAsync(int firmId, int userId, TagDto dto)
        {
            var response = await PostAsync<TagDto, DataResponseWrapper<long>>($"/Save?firmId={firmId}&userId={userId}", dto).ConfigureAwait(false);
            return response.Data;
        }

        public Task AttachTagsAsync(int firmId, int userId, AttachTagsRequestDto dto)
        {
            return PostAsync($"/AttachTags?firmId={firmId}&userId={userId}", dto);
        }

        public Task DetachTagsAsync(int firmId, int userId, DetachTagsRequestDto dto)
        {
            return PostAsync($"/DetachTags?firmId={firmId}&userId={userId}", dto);
        }

        public Task DetachTagsAsync(int firmId, int userId, EntityListRequestDto dto)
        {
            return PostAsync($"/DetachAllTags?firmId={firmId}&userId={userId}", dto);
        }

        public Task UpdateEntityNonSystemTagsAsync(int firmId, int userId, UpdateEntityTagsRequestDto dto)
        {
            var request = new
            {
                Tags = dto.TagIds,
                dto.EntityId,
                dto.EntityType
            };
            return PostAsync($"/UpdateEntityNonSystemTags?firmId={firmId}&userId={userId}", request);
        }
    }
}
