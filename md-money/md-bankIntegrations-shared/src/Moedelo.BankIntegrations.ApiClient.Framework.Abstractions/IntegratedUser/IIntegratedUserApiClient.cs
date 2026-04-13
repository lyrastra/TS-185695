using Moedelo.BankIntegrations.ApiClient.Dto.BankOperation;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegratedUser;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.IntegratedUser
{
    public interface IIntegratedUserApiClient
    {
        /// <summary> Создать интеграционного пользователя </summary>
        /// <param name="dto"> Входные параметры </param>
        /// <returns> Значение обновления </returns>
        Task<int> InsertAsync(IntegratedUserBaseDto dto);
        
        /// <summary> Получить последнего активного интеграционного пользователя по партнёру </summary>
        /// <param name="partner"> Идентификатор партнёра </param>
        /// <returns> Последней активный интеграционный пользователь </returns>
        Task<IntegratedUserDto> GetLastIntegratedUserAsync(IntegrationPartners partner);

        /// <summary> Получить интеграционную информацию </summary>
        /// <param name="partner"> Идентификатор партнёра </param>
        /// <param name="firmId"> Идентификатор фирмы </param>
        /// <returns> Интеграционная информация </returns>
        Task<string> GetIntegrationDataAsync(IntegrationPartners partner, int firmId);

        /// <summary> Обновить интеграционную информацию </summary>
        /// <param name="dto"> Входные параметры </param>
        /// <returns> Значение обновления </returns>
        Task<int> UpdateIntegrationDataAsync(UpdateIntegrationDataRequestDto dto);

        /// <summary> Отключить интеграцию </summary>
        /// <param name="dto"> Входные параметры </param>
        Task DisableIntegrationAsync(DisableIntegrationRequestDto dto);

        /// <summary> Получить интеграционного пользователя </summary>
        /// <param name="firmId"> Идентификатор фирмы </param>
        /// <param name="partner"> Идентификатор партнёра </param>
        /// <returns> Интеграционный пользователь </returns>
        Task<IntegratedUserDto> GetByFirmIdAsync(int firmId, IntegrationPartners partner);

        /// <summary> Сохранить интеграционного пользователя для тестера </summary>
        /// <param name="dto"> Входные параметры </param>
        Task SaveForTesterAsync(SaveForTesterDto dto);

        /// <summary> Получить список активных пользователей для заданного партнёра </summary>
        /// <param name="integrationPartner"> Идентификатор партнёра </param>
        /// <param name="pageNumber"> Номер страницы (начиная с 1) </param>
        /// <param name="pageSize"> Размер страницы </param>
        /// <param name="isActive"> Фильтровать по значению IsActive. null - строки не фильтруются по IsActive, true|false - только записи с указанных значением IsActive</param>
        /// <returns> Интеграционные пользователи и страница </returns>
        Task<IntegratedUsersPageDto> GetPartnerIntegratedUsersAsync(
            IntegrationPartners integrationPartner,
            int pageNumber,
            uint pageSize,
            bool? isActive);

        /// <summary> Получить список активных интеграций пользователя по идентификаторам фирм </summary>
        /// <param name="firmIds"> Идентификаторы фирм </param>
        /// <returns> Активные интеграции по фирме </returns>
        Task<List<FirmIntegrationPartnerDto>> GetActiveIntegrationsForFirmsAsync(IReadOnlyCollection<int> firmIds);

        /// <summary> Получить список активных интеграций пользователя с вложенными изображениями </summary>
        /// <param name="firmId"> Идентификатор фирмы </param>
        /// <returns> Список активных интеграций пользователя с изображениями </returns>
        Task<List<IntegrationPartnersInfoDto>> GetActiveIntegrationsPartnersInfoAsync(int firmId);

        /// <summary> Получить id прайс-листа для подписок </summary>
        /// <param name="firmId"> Идентификатор фирмы </param>
        /// <param name="partner"> Идентификатор партнёра </param>
        /// <returns> Значение прайс-листа </returns>
        Task<int> GetAcceptancePriceListAsync(int firmId, IntegrationPartners partner);
        
        /// <summary> Установить id прайс-листа для подписок </summary>
        /// <param name="firmId"> Идентификатор фирмы </param>
        /// <param name="priceListId"> Идентификатор прайс-листа </param>
        /// <param name="partner"> Идентификатор партнёра </param>
        Task SetAcceptancePriceListAsync(int firmId, int priceListId, IntegrationPartners partner);

        Task<List<IntegrationPartners>> GetActiveIntegrationsForSendPaymentAsync(int firmId);

        /// <summary> Подучить интеграционного пользователя по внешнему идентификатору </summary>
        /// <param name="externalSystemId"> Идентификатор внешней системы/банка-партнёра </param>
        /// <param name="integrationPartner"> Идентификатор фирмы </param>
        /// <returns> Интеграционный пользователя </returns>
        Task<IntegratedUserDto> GetIntegratedUserByIdInExternalSystemAsync(string externalSystemId, IntegrationPartners integrationPartner);

        /// <summary> Подучить наличие патента </summary>
        /// <param name="firmId"> Идентификатор фирмы </param>
        /// <param name="partner"> Идентификатор партнёра </param>
        /// <returns> Интеграционный пользователя </returns>
        Task<bool> GetIsPatentAsync(int firmId, IntegrationPartners partner);

        Task CreateOrUpdateAsync(IntegratedUserRequestDto requestDto);

        /// <summary> Удалить интеграционного пользователя </summary>
        /// <param name="integratedUserId"> Идентификатор записи </param>
        Task DeleteAsync(int integratedUserId);

        /// <summary> Сохранить интеграционного пользователя </summary>
        /// <param name="request"> Входные параметры </param>
        Task SaveAsync(IntegratedUserDto request);

        /// <summary> Установить попытку выставления ПТ в IntegrationData </summary>
        /// <param name="request"> Входные параметры </param>
        Task SetAcceptanceLastErrorAsync(AcceptanceLastErrorDto request);

        /// <summary> Установить блокировку на р/с по ЗДА в IntegrationData </summary>
        /// <param name="request"> Входные параметры </param>
        Task SetAcceptanceBlockAsync(AcceptanceBlockDto request);

        /// <summary> Получить интеграционные идентичности </summary>
        /// <param name="request"> Входные параметры </param>
        /// <returns> Список интеграционных идентичностей </returns>
        Task<List<IntegrationIdentityDto>> IntegrationTurnGetIdentitiesAsync(IntegrationIdentityDto request);

        /// <summary> Сбросить прайс-лист в IntegrationData </summary>
        /// <param name="integratorId"> Идентификатор партнёра </param>
        /// <param name="firmId"> Идентификатор фирмы </param>
        /// <returns> Флаг сброса </returns>
        Task<bool> ResetAcceptancePriceListAsync(int integratorId, int firmId);
    }
}
