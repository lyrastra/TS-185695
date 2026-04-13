using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.RptV2.Dto.ReportSettings;

namespace Moedelo.RptV2.Client.RptSettings
{
    public interface IRptSettingsApiClient : IDI
    {
        Task Save(int firmId, int userId, RptSettingsSaveRequest request);
        Task<RptSettingsDto> GetAsync(int firmId, int userId);
    }
}