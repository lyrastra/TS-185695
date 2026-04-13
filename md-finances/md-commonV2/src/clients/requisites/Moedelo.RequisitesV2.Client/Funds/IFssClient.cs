using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.RequisitesV2.Dto.Funds;

namespace Moedelo.RequisitesV2.Client.Funds
{
    public interface IFssClient : IDI
    {
        Task<FssDepartmentDto> GetDepartmentAsync(int firmId, int userId);

        Task SaveDepartmentAsync(int firmId, int userId, FssDepartmentDto department);

        Task<List<FssRateDto>> GetRatesAsync(int firmId, int userId);

        Task<FssRateDto> GetRateAsync(int firmId, int userId, int year);

        Task SaveRatesAsync(int firmId, int userId, List<FssRateDto> rates);
    }
}