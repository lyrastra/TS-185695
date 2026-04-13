using Moedelo.Authorities.ApiClient.Abstractions.Sfr.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Authorities.ApiClient.Abstractions.Sfr
{
    public interface ISfrDepartmentsApiClient
    {
        Task<IReadOnlyCollection<SfrDepartmentDto>> GetByRegionCodeAsync(string regionCode);

        Task<SfrDepartmentDto> GetByCodeAsync(string code);

        Task<SfrDepartmentDto> GetByIdAsync(int Id);
    }
}
