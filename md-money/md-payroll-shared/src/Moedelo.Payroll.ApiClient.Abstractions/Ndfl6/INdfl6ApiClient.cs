using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Payroll.ApiClient.Abstractions.Ndfl6.Dto;

namespace Moedelo.Payroll.ApiClient.Abstractions.Ndfl6
{
    public interface INdfl6ApiClient
    {
        Task<Ndfl6DataDto> GetDataAsync(FirmId firmId, UserId userId, int year, int quarter, int ndflSettingId);
        Task<IReadOnlyCollection<Ndfl6NdflSettingDto>> GetNdflSettingsAsync(FirmId firmId, UserId userId, 
            int year, int quarter);
    }
}