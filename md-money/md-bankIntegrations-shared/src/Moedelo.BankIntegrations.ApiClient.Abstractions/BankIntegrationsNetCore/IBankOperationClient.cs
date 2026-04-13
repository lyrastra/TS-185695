using System;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationMonitor;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.BankOperation.PaymentRegistry;
using Moedelo.BankIntegrations.ApiClient.Dto.BankOperation;
using Moedelo.BankIntegrations.ApiClient.Dto.BankOperation.Invoice;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegratedUser;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.BankIntegrationsNetCore
{
    public interface IBankOperationClient
    {
        /// <summary>
        /// Получение информации по сертификату по конкретному партнеру
        /// </summary>
        Task<CertificateThumbprintDto> GetCertificateThumbprintByPartnerAsync(IntegrationPartners partner);

        /// <summary> Отправка сквозного п/п </summary>
        Task<Dto.BankOperation.IntegrationResponseDto<SendBankInvoiceResponseDto>> SendInvoiceAsync(SendBankInvoiceRequestDto dto);

        /// <summary>
        /// Создание черновика платежного реестра
        /// </summary>
        Task<SalaryPaymentRegistryCreationResponseDto> CreateSalaryPaymentRegistryAsync(SalaryPaymentRegistryCreationRequestDto dto);

        /// <summary>
        /// Запрос выписки по всем счетам фирмы по партнеру после включения интеграции
        /// </summary>
        Task RequestMovementsAfterTurnIntegrationByFirmAsync(RequestMovementForAllSettlementsDto dto);

        /// <summary>
        /// Отправляет запрос автоматических выписок за текущие сутки по партнёру.
        /// </summary>
        /// <param name="partner">Идентификатор банка-партнёра</param>
        /// <returns><c>true</c>, если запрос успешно поставлен в очередь; иначе <c>false</c></returns>
        Task<bool> SendAutoTodayRequestAsync(IntegrationPartners partner);
    }
}