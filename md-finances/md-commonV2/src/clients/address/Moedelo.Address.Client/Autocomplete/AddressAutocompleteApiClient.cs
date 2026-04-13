using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Address.Dto.Autocomplete;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Address.Client.Autocomplete
{
    [InjectAsSingleton]
    public class AddressAutocompleteApiClient : BaseApiClient, IAddressAutocompleteApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public AddressAutocompleteApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) : base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager
                )
        {
            apiEndPoint = settingRepository.Get("AddressApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<List<AddressObjectAutocompleteDto>> GetLocationAutocomplete(string query)
        {
            return PostAsync<string, List<AddressObjectAutocompleteDto>>("/Autocomplete/GetLocationAutocomplete", query);
        }

        public Task<List<AddressObjectAutocompleteDto>> GetLocationAutocompleteByKladr(string[] parentCodes, string query)
        {
            return PostAsync<LocationAutocompleteRequest, List<AddressObjectAutocompleteDto>>("/Autocomplete/GetLocationAutocompleteByKladr", new LocationAutocompleteRequest
            {
                Query = query,
                ParentCodes = parentCodes
            });
        }

        public Task<List<AddressObjectAutocompleteDto>> GetStreetAutocomplete(string parentCode, string query)
        {
            var request = new StreetAutocompleteRequest
            {
                ParentCode = parentCode,
                Query = query
            };
            return PostAsync<StreetAutocompleteRequest, List<AddressObjectAutocompleteDto>>("/Autocomplete/GetStreetAutocomplete", request);
        }

        public Task<List<HouseAutocompleteDto>> GetHouseAutocomplete(string parentCode, string query)
        {
            var request = new HouseAutocompleteRequest
            {
                ParentCode = parentCode,
                Query = query
            };
            return PostAsync<HouseAutocompleteRequest, List<HouseAutocompleteDto>>("/Autocomplete/GetHouseAutocomplete", request);
        }

        public Task<List<AddressObjectAutocompleteDto>> GetRegionAutocomplete(string query, int take)
        {
            var request = new RegionAutocompleteRequest
            {
                Query = query,
                Take = take
            };
            return PostAsync<RegionAutocompleteRequest, List<AddressObjectAutocompleteDto>>("/Autocomplete/GetRegionAutocomplete", request);
        }

        public Task<AddressIdentityTypesDto> GetAddressIdentityTypes()
        {
            return GetAsync<AddressIdentityTypesDto>("/Autocomplete/GetAddressIdentityTypes");
        }

        public Task<string> GetPostIndex(PostIndexRequest request)
        {
            return PostAsync<PostIndexRequest, string>("/Autocomplete/GetPostIndex", request);
        }

        public Task<string> GetFnsCode(FnsCodeRequest request)
        {
            return PostAsync<FnsCodeRequest, string>("/Autocomplete/GetFnsCode", request);
        }

        public Task<string> GetOktmo(OktmoRequest request)
        {
            return PostAsync<OktmoRequest, string>("/Autocomplete/GetOktmo", request);
        }


        public Task<string> GetAddressStringPreview(AddressPreviewRequest request)
        {
            return PostAsync<AddressPreviewRequest, string>("/Autocomplete/GetAddressStringPreview", request);
        }

        public Task<AddressPreviewDto> GetAddressPreview(AddressPreviewRequest request)
        {
            return PostAsync<AddressPreviewRequest, AddressPreviewDto>("/Autocomplete/GetAddressPreview", request);

        }
    }
}
