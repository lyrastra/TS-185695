using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Address.ApiClient.Abstractions;
using Moedelo.Address.ApiClient.Abstractions.Address;
using Moedelo.Address.ApiClient.Abstractions.Address.Dto;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Address.ApiClient.NetFramework.Address
{
    [InjectAsSingleton(typeof(IFirmAddressApiClient))]
    public class FirmAddressApiClient : BaseCoreApiClient, IFirmAddressApiClient
    {
        private readonly SettingValue apiEndPoint;
        private const string Prefix = "/private/api/v1/";

        public FirmAddressApiClient(
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
            return apiEndPoint.Value + Prefix;
        }

        public async Task SaveAsync(int firmId, AddressSaveDto dto)
        {
            var tokenHeaders = await GetUnidentifiedTokenHeaders().ConfigureAwait(false);
            await PostAsync($"/firm/{firmId}", dto, queryHeaders: tokenHeaders).ConfigureAwait(false);
        }

        public async Task<AddressGetDto> GetAsync(int firmId, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            var tokenHeaders = await GetUnidentifiedTokenHeaders(ct).ConfigureAwait(false);
            var result = await GetAsync<ApiDataResult<AddressGetDto>>($"/firm/{firmId}", queryHeaders: tokenHeaders, cancellationToken: ct).ConfigureAwait(false);

            return result.data;
        }

        public async Task<IReadOnlyDictionary<int, AddressGetDto>> GetListAsync(IReadOnlyCollection<int> firmIds, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            if (firmIds?.Any() != true)
            {
                return new Dictionary<int, AddressGetDto>();
            }

            var tokenHeaders = await GetUnidentifiedTokenHeaders(ct).ConfigureAwait(false);
            var result = await PostAsync<IReadOnlyCollection<int>, ApiDataResult<IReadOnlyDictionary<int, AddressGetDto>>>(
                "/firm/GetList",
                firmIds,
                queryHeaders: tokenHeaders,
                cancellationToken: ct).ConfigureAwait(false);

            return result.data;
        }

        public async Task<string> GetStringAsync(int firmId, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            var uri = $"firm/{firmId}/GetString";

            var httpSetting = new HttpQuerySetting
            {
                DontThrowOn404 = true
            };
            
            var tokenHeaders = await GetUnidentifiedTokenHeaders(ct).ConfigureAwait(false);
            var result = await GetAsync<ApiDataResult<string>>(uri, queryHeaders: tokenHeaders, setting: httpSetting, cancellationToken: ct)
                .ConfigureAwait(false);

            return result.data;
        }

        public async Task<string> GetStringWithAdditionalInfoAsync(int firmId, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            var tokenHeaders = await GetUnidentifiedTokenHeaders(ct).ConfigureAwait(false);
            var result = await GetAsync<ApiDataResult<string>>($"firm/{firmId}/GetStringWithAdditionalInfo", queryHeaders: tokenHeaders, cancellationToken: ct).ConfigureAwait(false);

            return result.data;
        }

        public async Task<IReadOnlyDictionary<int, string>> GetStringsAsync(IReadOnlyCollection<int> firmIds, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            if (firmIds == null || firmIds.Count == 0)
            {
                return new Dictionary<int, string>();
            }

            var tokenHeaders = await GetUnidentifiedTokenHeaders(ct).ConfigureAwait(false);
            var result = await PostAsync<IReadOnlyCollection<int>, ApiDataResult<IReadOnlyDictionary<int, string>>>(
                "/firm/GetStrings",
                firmIds,
                queryHeaders: tokenHeaders,
                cancellationToken: ct).ConfigureAwait(false);

            return result.data;
        }
    }
}
