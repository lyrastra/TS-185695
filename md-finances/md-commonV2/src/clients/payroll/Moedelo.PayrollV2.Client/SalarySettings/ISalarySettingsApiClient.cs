using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.PayrollV2.Client.SalarySettings.DTO;

namespace Moedelo.PayrollV2.Client.SalarySettings
{
    public interface ISalarySettingsApiClient : IDI
    {
        Task<SalarySettingPrivateClientModel> GetSalarySettingData(int firmId, int userId);

        Task<SalarySettingDto> GetSalarySetting(int firmId, int userId);

        Task SetPilotProjectStartDate(int firmId, int userId);

        Task SaveSalarySettingData(int firmId, int userId, SavingSalarySettings settingData);
    }
}