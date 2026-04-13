using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Address.ApiClient.Abstractions.legacy.Dto.Autocomplete;

namespace Moedelo.Address.ApiClient.Abstractions.legacy
{
    public interface IAddressAutocompleteV2ApiClient
    {
        Task<List<StreetAutocompleteV2Response>> GetStreetAutocompleteAsync(Guid parentGuid, string query, string parentName);

        Task<List<LocationAutocompleteV2Response>> GetLocationAutocompleteAsync(string query);

        Task<AddressParamsResponse> GetAddressParamsAsync(bool isOoo, Guid guid, string house, string building, string buildingName);
    }
}
