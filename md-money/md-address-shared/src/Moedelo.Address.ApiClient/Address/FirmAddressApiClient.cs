using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Address.ApiClient.Abstractions;
using Moedelo.Address.ApiClient.Abstractions.Address;
using Moedelo.Address.ApiClient.Abstractions.Address.Dto;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Address.ApiClient.Address
{
    [InjectAsSingleton(typeof(IFirmAddressApiClient))]
    public class FirmAddressApiClient : BaseApiClient, IFirmAddressApiClient
    {
        private const string Prefix = "/private/api/v1/firm";

        public FirmAddressApiClient(
            IHttpRequestExecuter httpRequestExecutor,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<FirmAddressApiClient> logger)
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

        public async Task<AddressGetDto> GetAsync(int firmId, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            var result = await GetAsync<ApiDataResult<AddressGetDto>>($"{Prefix}/{firmId}", cancellationToken: ct);

            return result.data;
        }

        public async Task<IReadOnlyDictionary<int, AddressGetDto>> GetListAsync(IReadOnlyCollection<int> firmIds, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (firmIds?.Any() != true)
            {
                return new Dictionary<int, AddressGetDto>();
            }

            var result = await PostAsync<IReadOnlyCollection<int>, ApiDataResult<IReadOnlyDictionary<int, AddressGetDto>>>(
                $"{Prefix}/GetList",
                firmIds,
                cancellationToken: cancellationToken);

            return result.data;
        }

        public async Task<string> GetStringAsync(int firmId, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            var result = await GetAsync<ApiDataResult<string>>($"{Prefix}/{firmId}/GetString", cancellationToken: ct);

            return result.data;
        }

        public Task<string> GetStringWithAdditionalInfoAsync(int firmId, CancellationToken ct)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyDictionary<int, string>> GetStringsAsync(IReadOnlyCollection<int> firmIds, CancellationToken ct)
        {
            throw new System.NotImplementedException();
        }

        public Task SaveAsync(int firmId, AddressSaveDto dto)
        {
            return PostAsync<AddressSaveDto, ApiDataResult<long>>($"{Prefix}/{firmId}", dto);
        }
    }
}