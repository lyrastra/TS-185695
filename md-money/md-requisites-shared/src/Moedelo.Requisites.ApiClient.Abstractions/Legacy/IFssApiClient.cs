using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy
{
    public interface IFssApiClient
    {
        Task<FssDepartmentDto> GetDepartmentAsync(FirmId firmId, UserId userId);

        Task SaveDepartmentAsync(FirmId firmId, UserId userId, FssDepartmentDto department);
    }
}
