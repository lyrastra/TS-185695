using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.SalarySettings;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy
{
    public interface ISalarySettingsApiClient
    {
        /// <summary>
        /// Получить настройки ЗП и создать если их нет
        /// </summary>
        Task<SalarySettingDto> GetSalarySetting(int firmId, int userId);

        /// <summary>
        /// Получить настройки ЗП или настройки по-умолчанию если настройки не созданы
        /// </summary>
        Task<SalarySettingDto> GetSalarySettingOrDefaultByFirmIdAsync(int firmId);

        Task<NdflSettingDto> GetNdflSetting(int firmId, int userId, int ndflSettingId);

        Task<IReadOnlyCollection<NdflSettingDto>> GetNdflSettingList(int firmId, int userId);

    }
}