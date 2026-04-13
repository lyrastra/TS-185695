using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Address.ApiClient.Abstractions.legacy;
using Moedelo.Address.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;


namespace Moedelo.Address.ApiClient.legacy
{
    [InjectAsSingleton(typeof(IAddressApiClient))]
    internal class AddressApiClient : BaseApiClient, IAddressApiClient
    {
        public AddressApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<AddressApiClient> logger)
            : base(
                  httpRequestExecuter,
                  uriCreator,
                  auditTracer,
                  authHeadersGetter,
                  auditHeadersGetter,
                  settingRepository.Get("AddressApiEndpoint"),
                  logger)
        {
        }

        public Task<AddressDto> GetFirmAddressAsync(int firmId)
        {
            return GetAsync<AddressDto>("/GetFirmAddress", new { firmId });
        }
        
        public Task<string> GetFirmAddressStringAsync(int firmId, bool withAdditionalInfo)
        {
            return GetAsync<string>("/GetFirmAddressString", new { firmId, withAdditionalInfo });
        }

        public Task<AddressDto> GetAddressAsync(long id)
        {
            return GetAsync<AddressDto>("/GetAddress", new { id });
        }

        public Task<string> GetAddressStringAsync(long id, bool withAdditionalInfo)
        {
            return GetAsync<string>("/GetAddressString", new { id, withAdditionalInfo });
        }

        public Task<List<AddressDto>> GetAddressListAsync(List<long> ids)
        {
            return PostAsync<List<long>, List<AddressDto>>("/GetAddressList", ids);
        }

        public Task<List<AddressWithGuidsDto>> GetAddressListWithGuids(List<long> ids)
        {
            return PostAsync<List<long>, List<AddressWithGuidsDto>>("/GetAddressListWithGuids", ids);
        }

        public Task<List<string>> GetFirmAddressStringListAsync(List<int> firmIds, bool withAdditionalInfo)
        {
            var dto = new GetFirmAddressStringListDto { FirmIds = firmIds, WithAdditionalInfo = withAdditionalInfo };
            return PostAsync<GetFirmAddressStringListDto, List<string>>("/GetFirmAddressStringList", dto);
        }

        public Task SaveFirmAddressAsync(AddressSaveDto dto)
        {
            return PostAsync("/SaveFirmAddress", dto);
        }

        public Task<long> SetAddress(AddressSaveDto dto)
        {
            return PostAsync<AddressSaveDto, long>("/SaveAddress", dto);
        }
        
        public Task<AddressObjectDto> GetAddressObject(string kladrCode)
        {
            return GetAsync<AddressObjectDto>("/GetAddressObject", new { kladrCode });
        }

        public Task<List<AddressDto>> GetAddressListByCodesAsync(List<string> codes)
        {
            if (codes.Count == 0)
            {
                return Task.FromResult(new List<AddressDto>());
            }

            return PostAsync<List<string>, List<AddressDto>>("/GetAddressListByCodes", codes);
        }

        public Task<HouseDto> GetHouseByAoGuidAndNum(HouseGetByRequestDto dto)
        {
            return GetAsync<HouseDto>("/House/GetByAoGuidAndNumber", dto);
        }

        public Task<AddressDto> GetByAoGuidAsync(string aoGuid)
        {
            return GetAsync<AddressDto>("/GetByAoGuid", new { aoGuid });
        }

        public Task<AddressDto> GetByHouseGuidAsync(string houseGuid)
        {
            return GetAsync<AddressDto>("/GetByHouseGuid", new { houseGuid });
        }

        public Task<IReadOnlyDictionary<int, AddressDto>> GetFirmAddressDictionaryAsync(IReadOnlyCollection<int> firmIds, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (firmIds?.Any() != true)
            {
                return Task.FromResult<IReadOnlyDictionary<int, AddressDto>>(new Dictionary<int, AddressDto>());
            }

            return PostAsync<IReadOnlyCollection<int>, IReadOnlyDictionary<int, AddressDto>>("/GetFirmAddressDictionary", 
                firmIds, 
                cancellationToken: cancellationToken);
        }
    }
}
