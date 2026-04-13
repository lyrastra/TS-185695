using Moedelo.Authorities.ApiClient.Abstractions.Sfr.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Authorities.ApiClient.Abstractions.Sfr
{
    public interface ISfrAutocompleteApiClient
    {
        Task<IReadOnlyCollection<SfrDepartmentWithRequisitesAutocompleteItemDto>> 
            GetDepartmentWithRequisitesByCodeAsync(string query, int count = 5);
    }
}
