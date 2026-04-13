using Moedelo.Common.Types;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using System.Threading.Tasks;

namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy
{
    public interface IRosstatApiClient
    {
        Task<RosstatDepartmentDto> GetDepartmentAsync(FirmId firmId, UserId userId);
        Task SaveDepartmentAsync(FirmId firmId, UserId userId, RosstatDepartmentDto department);
    }
}
