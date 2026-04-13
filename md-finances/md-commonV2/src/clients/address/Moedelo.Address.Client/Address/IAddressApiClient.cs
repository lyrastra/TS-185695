using System.Collections.Generic;
using Moedelo.Address.Dto.Address;
using System.Threading.Tasks;
using System;
using System.Threading;

namespace Moedelo.Address.Client.Address
{
    [Obsolete("Необходимо использовать IFirmAddressApiClient или IAddressApiClient из Moedelo.Address.ApiClient.Abstractions.Address")]
    public interface IAddressApiClient
    {
        Task<string> GetFirmAddressString(int firmId, bool withAdditionalInfo = false);
        Task<AddressDto> GetFirmAddress(int firmId);
        Task<AddressV2Dto> GetFirmV2AddressAsync(int firmId, CancellationToken ctx = default);
        Task<List<string>> GetFirmAddressStringList(List<int> firmIds, bool withAdditionalInfo = false);
        Task<List<AddressDto>> GetFirmAddressList(List<int> firmIds);
        Task SetFirmAddress(AddressSaveDto dto);
        Task<string> GetAddressString(long id, bool withAdditionalInfo = false);
        Task<List<string>> GetAddressStringList(List<long> ids, bool withAdditionalInfo = false);
        Task<AddressDto> GetAddress(long id);
        Task<List<AddressDto>> GetAddressList(List<long> ids);
        Task<long> SetAddress(AddressSaveDto dto);
        Task<List<AddressDto>> GetAddressListByCodesAsync(List<string> codes);
        Task<AddressObjectDto> GetAddressObject(string kladrCode);
        Task DeleteFirmAddressAsync(int firmId);
        Task DeleteAddressAsync(long addressId, int firmId);
    }
}
