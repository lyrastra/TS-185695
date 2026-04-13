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
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Address.ApiClient.NetFramework.Address
{
    [InjectAsSingleton(typeof(IAddressApiClient))]
    public class AddressApiClient : BaseCoreApiClient, IAddressApiClient
    {
        private readonly SettingValue apiEndPoint;
        private const string Prefix = "/private/api/v1";

        public AddressApiClient(
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

        public async Task<AddressGetDto> GetAsync(long id, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            var result = await GetAsync<ApiDataResult<AddressGetDto>>($"/{id}", cancellationToken: ct).ConfigureAwait(false);

            return result.data;
        }

        public async Task<IReadOnlyDictionary<long, AddressGetDto>> GetAsync(IReadOnlyCollection<long> ids)
        {
            if (ids == null || ids.Count == 0)
            {
                return new Dictionary<long, AddressGetDto>();
            }

            var result = await PostAsync<IReadOnlyCollection<long>, ApiDataResult<IReadOnlyDictionary<long, AddressGetDto>>>("/GetList", ids).ConfigureAwait(false);

            return result.data;
        }

        public async Task<string> GetStringAsync(long id, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            var result = await GetAsync<ApiDataResult<string>>($"/GetString/{id}", cancellationToken: ct).ConfigureAwait(false);

            return result.data;
        }

        public async Task<string> GetStringWithAdditionalInfoAsync(long id, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            var result = await GetAsync<ApiDataResult<string>>($"/GetStringWithAdditionalInfo/{id}", cancellationToken: ct).ConfigureAwait(false);

            return result.data;
        }

        public async Task<long> CreateAsync(int firmId, AddressSaveDto dto)
        {
            var result = await PostAsync<AddressSaveDto, ApiDataResult<long>>($"/{firmId}", dto).ConfigureAwait(false);

            return result.data;
        }

        public async Task<IReadOnlyDictionary<long, AddressGetDto>> CreateListAsync(int firmId, IReadOnlyCollection<AddressSaveDto> dtos)
        {
            if (dtos == null || dtos.Count == 0)
            {
                return new Dictionary<long, AddressGetDto>();
            }

            var result = await PostAsync<IReadOnlyCollection<AddressSaveDto>, ApiDataResult<IReadOnlyDictionary<long, AddressGetDto>>>($"/Addresses/{firmId}", dtos).ConfigureAwait(false);

            return result.data;
        }

        public async Task UpdateAsync(long id, AddressSaveDto dto)
        {
            await PutAsync($"/{id}", dto).ConfigureAwait(false);
        }

        public async Task DeleteAsync(long id, int firmId)
        {
            await DeleteAsync($"/{firmId}/{id}").ConfigureAwait(false);
        }

        public async Task DeleteListAsync(IReadOnlyCollection<long> addressList, int firmId)
        {
            if (addressList == null || !addressList.Any())
            {
                return;
            }

            await DeleteByRequestAsync($"/Addresses/{firmId}", addressList).ConfigureAwait(false);
        }
    }
}
