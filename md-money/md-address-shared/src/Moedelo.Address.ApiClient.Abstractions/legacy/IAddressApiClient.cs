using System.Collections.Generic;
using Moedelo.Address.ApiClient.Abstractions.legacy.Dto;
using System.Threading.Tasks;
using System.Threading;

namespace Moedelo.Address.ApiClient.Abstractions.legacy
{
    public interface IAddressApiClient
    {
        Task<AddressDto> GetFirmAddressAsync(int firmId);

        Task<string> GetFirmAddressStringAsync(int firmId, bool withAdditionalInfo);

        Task<AddressDto> GetAddressAsync(long id);

        Task<string> GetAddressStringAsync(long id, bool withAdditionalInfo);

        Task<List<AddressDto>> GetAddressListAsync(List<long> ids);

        Task<List<AddressWithGuidsDto>> GetAddressListWithGuids(List<long> ids);

        Task<List<string>> GetFirmAddressStringListAsync(List<int> firmIds, bool withAdditionalInfo);

        Task SaveFirmAddressAsync(AddressSaveDto dto);

        Task<long> SetAddress(AddressSaveDto dto);

        Task<AddressObjectDto> GetAddressObject(string kladrCode);

        Task<List<AddressDto>> GetAddressListByCodesAsync(List<string> codes);

        Task<HouseDto> GetHouseByAoGuidAndNum(HouseGetByRequestDto dto);

        Task<AddressDto> GetByAoGuidAsync(string aoGuid);

        Task<AddressDto> GetByHouseGuidAsync(string houseGuid);

        Task<IReadOnlyDictionary<int, AddressDto>> GetFirmAddressDictionaryAsync(IReadOnlyCollection<int> firmIds, CancellationToken cancellationToken);
    }
}
