using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Address.ApiClient.Abstractions;
using Moedelo.Address.ApiClient.Abstractions.Suggestion;
using Moedelo.Address.ApiClient.Abstractions.Suggestion.Dto;
using Moedelo.Address.Enums;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Address.ApiClient.Suggestion
{
    [InjectAsSingleton(typeof(IAddressSuggestionApiClient))]
    public class AddressSuggestionApiClient : BaseApiClient, IAddressSuggestionApiClient
    {
        private const string BasePrefix = "/suggestion/private/api/v1";

        public AddressSuggestionApiClient(
            IHttpRequestExecuter httpRequestExecutor,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<AddressSuggestionApiClient> logger)
            : base(
                httpRequestExecutor,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("AddressRestApiEndpoint"),
                logger)
        {
        }

        private static string GetPrefix(DivisionType divisionType)
        {
            return $"{BasePrefix}/{divisionType.ToString()}";
        }

        public async Task<SuggestionAddressWithoutBuildingDto> GetByGuidAsync(Guid guid, DivisionType divisionType, CancellationToken ct)
        {
            var result = await GetAsync<ApiDataResult<SuggestionAddressWithoutBuildingDto>>($"{GetPrefix(divisionType)}/byGuid/{guid}");

            return result.data;
        }

        public async Task<SuggestionAddressDto> GetByHouseGuidAsync(Guid guid, DivisionType divisionType, CancellationToken ct)
        {
            var result = await GetAsync<ApiDataResult<SuggestionAddressDto>>($"{GetPrefix(divisionType)}/byHouseGuid/{guid}");

            return result.data;
        }

        public Task<SuggestionAddressDto[]> GetByQueryAsync(DivisionType type, string query, int count, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
