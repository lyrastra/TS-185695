using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Address.Dto.Autocomplete;

namespace Moedelo.Address.Client.Autocomplete
{
    public interface IAddressAutocompleteV2ApiClient
    {
        Task<List<StreetAutocompleteV2Response>> GetStreetAutocompleteAsync(Guid parentGuid, string query, string parentName);
        Task<List<LocationAutocompleteV2Response>> GetLocationAutocompleteAsync(string query);
        Task<AddressParamsResponse> GetAddressParamsAsync(bool isOoo, Guid guid, string house, string building, string buildingName);
    }
}
