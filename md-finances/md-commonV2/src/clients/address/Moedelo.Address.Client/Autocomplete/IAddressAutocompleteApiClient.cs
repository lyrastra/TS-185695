using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Address.Dto.Autocomplete;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Address.Client.Autocomplete
{
    public interface IAddressAutocompleteApiClient : IDI
    {
        [Obsolete("Use AddressAutocompleteV2ApiClient.GetStreetAutocompleteAsync")]
        Task<List<AddressObjectAutocompleteDto>> GetStreetAutocomplete(string parentCode, string query);
        Task<List<HouseAutocompleteDto>> GetHouseAutocomplete(string parentCode, string query);
        Task<List<AddressObjectAutocompleteDto>> GetRegionAutocomplete(string query, int take);
        Task<AddressIdentityTypesDto> GetAddressIdentityTypes();

        [Obsolete("Use AddressAutocompleteV2ApiClient.GetAddressParamsAsync")]
        Task<string> GetPostIndex(PostIndexRequest request);
        Task<string> GetFnsCode(FnsCodeRequest request);

        [Obsolete("Use AddressAutocompleteV2ApiClient.GetAddressParamsAsync")]
        Task<string> GetOktmo(OktmoRequest request);
        Task<string> GetAddressStringPreview(AddressPreviewRequest request);
        Task<AddressPreviewDto> GetAddressPreview(AddressPreviewRequest request);

        [Obsolete("Use AddressAutocompleteV2ApiClient.GetLocationAutocompleteAsync")]
        Task<List<AddressObjectAutocompleteDto>> GetLocationAutocomplete(string query);
        Task<List<AddressObjectAutocompleteDto>> GetLocationAutocompleteByKladr(string[] parentCodes, string query);
    }
}
