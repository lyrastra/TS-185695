using Moedelo.BankIntegrationsV2.Dto.BankOperation;
using Moedelo.BankIntegrationsV2.Dto.Integrations;
using Moedelo.Common.Enums.Enums.Integration;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IntegrationPartners = Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums.IntegrationPartners;

namespace Moedelo.BankIntegrationsV2.Client.BankOperation
{
    public interface IBankOperationClientV2 : IDI
    {
        /// <summary> Вкл\выкл интеграцию клиенту </summary>
        Task<IntegrationTurnResponseDto> IntegrationTurnAsync(IntegrationTurnRequestDto dto, HttpQuerySetting setting = null);

        /// <summary> Отправка п/п в банк </summary>
        /// <param name="paymentOrders"> Отправляемые п/п </param>
        /// <param name="identity"> Информация клиента </param>
        Task<SendPaymentOrderResponseData> SendPaymentOrdersAsync(List<PaymentOrderDto> paymentOrders, IntegrationIdentityDto identity);

        /// <summary> Запрос выписки из банка </summary>
        Task<RequestMovementListResponseDto> RequestMovementListAsync(RequestMovementListRequestDto dto);

        /// <summary> Суммарная информация об остатках и оборотах за дату (Сбербанк) </summary>
        Task<SberbankStatementSummaryResponseDto> GetSberbankStatementSummaryAsync(SberbankStatementSummaryRequestDto dto);

        /// <summary> Послать основной запрос на выписку от лица всех пользователей интеграции (Многопоточный) </summary>
        Task<bool> SendDailyRequestAsync(DateTime beginDate, DateTime finishDate, int integrationPartner, bool withoutCheckCreated);

        /// <summary> Послать запрос на лаг выписку от лица всех пользователей интеграции </summary>
        Task<bool> SendDailyLagRequestAsync(DateTime beginDate, DateTime finishDate, int integrationPartner);

        /// <summary> Получение готовых выписок из банка по каждому пользователю конкретного партнёра </summary>
        Task<bool> GetReadyMovementListForAllUsersOfIntegration(int integrationPartner);

        /// <summary> Перезапрос пропущенных выписок </summary>
        Task<bool> SendReRequestAsync(int partner, DateTime startDate, DateTime endDate);
    }
}