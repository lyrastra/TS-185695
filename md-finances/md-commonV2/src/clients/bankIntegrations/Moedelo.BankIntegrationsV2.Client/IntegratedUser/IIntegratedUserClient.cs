using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using Moedelo.BankIntegrationsV2.Dto.IntegratedUser;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.BankIntegrationsV2.Client.IntegratedUser
{
    /// <summary> API для работы с внутренними связями МД юзера и банка-партнёра </summary>
    [Obsolete("Не использовать! Заменен на Moedelo.BankIntegrations.ApiClient.Framework.IntegratedUser")]
    public interface IIntegratedUserClient
    {
        /// <summary> Включить итеграцию через проект тестера </summary>
        Task SaveForTesterAsync(SaveForTesterDto dto);

        /// <summary> Включить итеграцию в рамках SSO </summary>
        /// Метод не перенесен в новый клиент, т.к. в будущем от него откажемся
        Task SaveFromSsoAsync(SaveFromSsoDto dto);

        /// <summary> Получить информацию по интеграции пользователя </summary>
        Task<IntegratedUserDto> GetIntegratedUserAsync(int firmId, IntegrationPartners integrationPartner);

        /// <summary>
        /// Получить список активных пользователей для заданного партнёра
        /// </summary>
        /// <param name="integrationPartner"></param>
        /// <param name="pageNumber">номер страницы (начиная с 1)</param>
        /// <param name="pageSize">размер страницы</param>
        /// <param name="isActive">фильтровать по значению IsActive. null - строки не фильтруются по IsActive, true|false - только записи с указанных значением IsActive</param>
        /// <param name="setting"></param>
        /// <returns></returns>
        Task<IntegratedUsersPageDto> GetPartnerIntegratedUsersAsync(
            IntegrationPartners integrationPartner,
            int pageNumber,
            uint pageSize,
            bool? isActive,
            HttpQuerySetting setting = null);

        /// <summary> Выключение интеграции пользователю </summary>
        Task DisableIntegrationAsync(DisableIntegrationRequestDto dto);

        /// <summary> Получить список включенных интегарций пользователя </summary>
        Task<List<IntegrationPartners>> GetActiveIntegrationsForFirmAsync(int firmId);

        Task<List<FirmIntegrationPartnerDto>> GetActiveIntegrationsForFirmsAsync(IReadOnlyCollection<int> firmIds);

        /// <summary> Получить список включенных интеграций пользователя с вложенными изображениями </summary>
        Task<List<IntegrationPartnersInfoDto>> GetActiveIntegrationsPartnersInfoAsync(int firmId);

        /// <summary> Получить id прайс-листа для подписок </summary>
        Task<int> GetAcceptancePriceListAsync(int firmId, IntegrationPartners partner);

        /// <summary> Установить id прайс-листа для подписок </summary>
        Task SetAcceptancePriceListAsync(NextAcceptancePriceDto nextAcceptancePriceDto);

        Task<List<IntegrationPartners>> GetActiveIntegrationsForSendPaymentAsync(int firmId);

        Task<IntegratedUserDto> GetIntegratedUserByIdInExternalSystem(string externalSystemId, IntegrationPartners integrationPartner);

        Task<bool> GetActiveIntegrationsLastSuccessfulSettlementRequestAsync(DateTime beginDate, DateTime endDate, int partner, string settlementNumber, int firmId);

        /// Метод не перенесен в новый клиент, т.к. используется только в консоле ЗДА 
        Task<IntegrationLimitsInfoDto> GetIntegrationLimitsAsync(int firmId);

        Task<bool> GetIsPatentAsync(int firmId, IntegrationPartners partner);

        Task<bool> ResetAcceptancePriceListAsync(int integratorId, int firmId);

        Task CreateAsync(IntegratedUserCreateRequestDto requestDto);

        Task UpdateAsync(IntegratedUserUpdateRequestDto requestDto);

        /// <summary>
        /// Удалить запись об интеграции пользователя с внешней системой
        /// </summary>
        /// <param name="integratedUserId">идентификатор записи</param>
        Task DeleteAsync(int integratedUserId);
    }
}
