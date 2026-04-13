using System;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Address.ApiClient.Abstractions;
using Moedelo.Address.ApiClient.Abstractions.Suggestion;
using Moedelo.Address.ApiClient.Abstractions.Suggestion.Dto;
using Moedelo.Address.Enums;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Address.ApiClient.NetFramework.Address
{
    [InjectAsSingleton(typeof(IAddressSuggestionApiClient))]
    public class AddressSuggestionApiClient : BaseCoreApiClient, IAddressSuggestionApiClient
    {
        private readonly SettingValue apiEndPoint;
        private const string PrivatePrefix = "/suggestion/private/api/v1";

        public AddressSuggestionApiClient(
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
            apiEndPoint = settingRepository.Get("AddressRestApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + PrivatePrefix;
        }

        public async Task<SuggestionAddressWithoutBuildingDto> GetByGuidAsync(Guid guid, DivisionType divisionType, CancellationToken ct = default)
        {
            var result = await GetAsync<ApiDataResult<SuggestionAddressWithoutBuildingDto>>($"/{divisionType}/byGuid/{guid}", cancellationToken: ct);

            return result.data;
        }

        public async Task<SuggestionAddressDto> GetByHouseGuidAsync(Guid guid, DivisionType divisionType, CancellationToken ct = default)
        {
            var result = await GetAsync<ApiDataResult<SuggestionAddressDto>>($"/{divisionType}/byHouseGuid/{guid}", cancellationToken: ct);

            return result.data;
        }

        public async Task<SuggestionAddressDto[]> GetByQueryAsync(DivisionType type, string query, int count, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return Array.Empty<SuggestionAddressDto>();
            }

            var result = await GetAsync<ApiDataResult<SuggestionAddressDto[]>>($"/{type}", new
            {
                query,
                count,
            }, cancellationToken: ct);

            return result.data;
        }
    }
}
