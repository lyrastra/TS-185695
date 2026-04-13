using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BankIntegrationsV2.Dto.DataInformation.Sberbank;
using Moedelo.BankIntegrationsV2.Dto.SberbankPaymentRequest;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.BankIntegrationsV2.Client.SberbankPaymentRequest
{
    /// <summary> Клиент для платёжных требований в Сбербанк </summary>
    public interface ISberbankPaymentRequestClient : IDI
    {
        /// <summary> Получение  </summary>
        Task<GetSberbankPaymentStatusResponseDto> GetPaymentRequestStatusAsync(GetPaymentRequestStatusRequestDto dto);

        /// <summary> Запросить список клиентов, участвующих(Подписался/Отписался) в подписке на требования </summary>
        Task<AdvanceAcceptanceReponseDto> GetAllAdvanceAcceptancesAsync();

        /// <summary> Запросить список подписанных ЗДА по фирме </summary>
        [Obsolete("Надо использовать GetAllowedAdvanceAcceptancesByFirmIdListAsync")]
        Task<AdvanceAcceptanceReponseDto> GetAllowedAdvanceAcceptancesByFirmAsync(int firmId);

        /// <summary> Запросить список подписанных ЗДА по фирме </summary>
        Task<AdvanceAcceptanceReponseDto> GetAllowedAdvanceAcceptancesByFirmIdListAsync(IReadOnlyCollection<int> firmIds);

        /// <summary> Обновить из Сбера в нашу БД moedelo список клиентов, участвующих(Подписался/Отписался) в подписке на требования </summary>
        Task SyncAdvanceAcceptancesAsync(SyncAdvanceAcceptancesRequestDto dto, HttpQuerySetting httpQuerySetting = null);

        Task<SendManualPaymentRequestResponseDto> SendManualPaymentRequest(SendManualPaymentRequestRequestDto dto, HttpQuerySetting httpQuerySetting = null);

        Task<AdvanceAcceptanceReponseDto> GetRawAcceptancesByInnAsync(string inn);

        Task<UpdateCreatedPaymentsStatusesResponseDto> UpdateCreatedPaymentsStatusesAsync(int limit, int minPayId, HttpQuerySetting httpQuerySetting = null);

        Task<GetTariffAndAcceptancePairInfoDto> GetTariffAndAcceptancePairInfo(int firmId, int tariffId);

        Task<VerifiedClientsByAcceptanceResponseDto> VerifiedClientsByAcceptanceAsync(int verifiedLimit = 0, int minAcceptanceId = 0, HttpQuerySetting httpQuerySetting = null);
        
        Task<List<SberbankIntegrationDataDto>> GetSberbankIntegrationDataByFirmIdListAsync(List<int> firmIdList);

        Task LinkAdvanceAcceptancesToFirmsAsync();

        Task<CategorizePaymentsResponseDto> GetCategorizePaymentsAsync(List<int> firmIds);
    }
}