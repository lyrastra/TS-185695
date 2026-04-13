using System.Linq;
using System.Collections.Generic;
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
    [InjectAsSingleton(typeof(IAddressApiClient))]
    public class AddressApiClient : BaseApiClient, IAddressApiClient
    {
        private const string Prefix = "/private/api/v1";

        public AddressApiClient(
            IHttpRequestExecuter httpRequestExecutor,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<AddressApiClient> logger)
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

        public async Task<AddressGetDto> GetAsync(long id, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            var result = await GetAsync<ApiDataResult<AddressGetDto>>($"{Prefix}/{id}", cancellationToken: ct);

            return result.data;
        }

        public async Task<IReadOnlyDictionary<long, AddressGetDto>> GetAsync(IReadOnlyCollection<long> ids)
        {
            var result = await PostAsync<IReadOnlyCollection<long>, ApiDataResult<IReadOnlyDictionary<long, AddressGetDto>>>($"{Prefix}/GetList", ids);

            return result.data;
        }

        public async Task<string> GetStringAsync(long id, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            var result = await GetAsync<ApiDataResult<string>>($"{Prefix}/GetString/{id}", cancellationToken: ct);

            return result.data;
        }

        public Task<string> GetStringWithAdditionalInfoAsync(long id, CancellationToken ct)
        {
            throw new System.NotImplementedException();
        }

        public async Task<long> CreateAsync(int firmId, AddressSaveDto dto)
        {
            var result = await PostAsync<AddressSaveDto, ApiDataResult<long>>($"{Prefix}/{firmId}", dto);

            return result.data;
        }

        public Task<IReadOnlyDictionary<long, AddressGetDto>> CreateListAsync(int firmId, IReadOnlyCollection<AddressSaveDto> dtos)
        {
            throw new System.NotImplementedException();
        }

        public async Task UpdateAsync(long id, AddressSaveDto dto)
        {
            await PutAsync($"{Prefix}/{id}", dto);
        }

        public Task DeleteAsync(long id, int firmId)
        {
            return DeleteAsync($"{Prefix}/{firmId}/{id}");
        }

        public Task DeleteListAsync(IReadOnlyCollection<long> addressList, int firmId)
        {
            if (addressList == null || addressList.Any())
            {
                return Task.CompletedTask;
            }

            return DeleteByRequestAsync($"/Addresses/{firmId}", addressList);
        }
    }
}
