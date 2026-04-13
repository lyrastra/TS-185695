using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.CatalogV2.Dto.ActivityCategory;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.CatalogV2.Client.ActivityCategory
{
    [InjectAsSingleton(typeof(IActivityCategoryApiClient))]
    public class ActivityCategoryApiClient : BaseApiClient, IActivityCategoryApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public ActivityCategoryApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager
                 )
        {
            apiEndPoint = settingRepository.Get("CatalogApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/ActivityCategory/V2";
        }

        public Task<ActivityCategoryDto> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return GetAsync<ActivityCategoryDto>("/GetById", new {id}, cancellationToken: cancellationToken);
        }

        public Task<List<ActivityCategoryDto>> GetByIdListAsync(IReadOnlyCollection<int> idList, CancellationToken cancellationToken)
        {
            return PostAsync<IReadOnlyCollection<int>, List<ActivityCategoryDto>>("/GetByIdList", idList, cancellationToken: cancellationToken);
        }

        public Task<ActivityCategoryDto> GetByCodeAndNameAsync(string code, string name, CancellationToken cancellationToken)
        {
            return GetAsync<ActivityCategoryDto>("/GetByCodeAndName", new {code, name}, cancellationToken: cancellationToken);
        }

        public Task<List<ActivityCategoryDto>> GetAutocompleteByCodeAsync(string code, int count, CancellationToken cancellationToken)
        {
            return GetAsync<List<ActivityCategoryDto>>("/GetAutocompleteByCode", new {code, count}, cancellationToken: cancellationToken);
        }

        public Task<List<ActivityCategoryDto>> GetAutocompleteByCodeOrNameAsync(string query, int count, CancellationToken cancellationToken)
        {
            const string uri = "/GetActivityCategoryAutocomplete";
            
            return GetAsync<List<ActivityCategoryDto>>(uri, new {query, count}, cancellationToken: cancellationToken);
            
        }

        public Task<List<ActivityCategoryDto>> GetByCodesAsync(IReadOnlyCollection<string> codeList, CancellationToken cancellationToken)
        {
            return PostAsync<IReadOnlyCollection<string>, List<ActivityCategoryDto>>("/GetByCodes", codeList, cancellationToken: cancellationToken);
        }
    }
}