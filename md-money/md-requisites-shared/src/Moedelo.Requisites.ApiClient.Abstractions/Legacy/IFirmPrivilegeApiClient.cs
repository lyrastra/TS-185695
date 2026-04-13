using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy
{
    public interface IFirmPrivilegeApiClient
    {
        Task<IReadOnlyList<FirmPrivilegeDto>> GetPrivilegesByYearAsync(int year, int skipCount, int takeCount);
        Task<IReadOnlyList<FirmPrivilegeDto>> GetPrivilegesByFirmIdAsync(int firmId);
    }
}