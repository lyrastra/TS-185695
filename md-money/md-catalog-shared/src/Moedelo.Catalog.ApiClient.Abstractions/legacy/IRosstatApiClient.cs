using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Catalog.ApiClient.Abstractions.legacy.Dto;

namespace Moedelo.Catalog.ApiClient.Abstractions.legacy
{
    public interface IRosstatApiClient
    {
        Task<List<RosstatDto>> GetDepartmentListByRegionCodeAsync(string regionCode);
    }
}