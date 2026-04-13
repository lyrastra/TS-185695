using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Address.ApiClient.Abstractions.legacy.Dto.FiasFormatAutocomplete;

namespace Moedelo.Address.ApiClient.Abstractions
{
    /// <summary>
    /// Клиент для получения адреса в иеархии ФИАС
    /// </summary>
    public interface IAddressSuggestionFiasFormatClient
    {
        Task<List<LocationAutocompleteFiasFormatResponse>> GetLocationAutocompleteAsync(int firmId, int userId, string query);

        Task<List<StreetAutocompleteFiasFormatResponse>> GetStreetAutocompleteAsync(int firmId, int userId, Guid parentGuid, string query, string parentName);

        Task<AddressParamsFiasFormatResponse> GetAddressParamsAsync(int firmId, int userId, AddressParamsFiasFormatRequest request);
    }
}
