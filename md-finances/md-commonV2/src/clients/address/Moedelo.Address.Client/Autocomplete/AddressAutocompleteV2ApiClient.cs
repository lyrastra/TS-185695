using System;
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
    [InjectAsSingleton(typeof(IAddressAutocompleteV2ApiClient))]
    public class AddressAutocompleteV2ApiClient : BaseApiClient, IAddressAutocompleteV2ApiClient
    {
        private readonly SettingValue apiEndPoint;

        public AddressAutocompleteV2ApiClient(
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

        public Task<List<StreetAutocompleteV2Response>> GetStreetAutocompleteAsync(Guid parentGuid, string query, string parentName)
        {
            var request = new StreetAutocompleteV2Request
            {
                ParentName = parentName,
                ParentGuid = parentGuid,
                Query = query
            };

            return PostAsync<StreetAutocompleteV2Request, List<StreetAutocompleteV2Response>>("/Autocomplete/v2/GetStreetAutocomplete", request);
        }

        public Task<List<LocationAutocompleteV2Response>> GetLocationAutocompleteAsync(string query)
        {
            return PostAsync<string, List<LocationAutocompleteV2Response>>("/Autocomplete/v2/GetLocationAutocomplete", query);
        }

        public Task<AddressParamsResponse> GetAddressParamsAsync(bool isOoo, Guid guid, string house, string building, string buildingName)
        {
            var request = new AddressParamsRequest
            {
                IsOoo = isOoo,
                Guid = guid,
                House = house,
                Building = building,
                BuildingName = buildingName
            };

            return PostAsync<AddressParamsRequest, AddressParamsResponse>("/Autocomplete/v2/GetAddressParams", request);
        }
    }
}
