using System;
using System.Collections.Generic;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.BankOperation.PaymentRegistry;

public class SalaryPaymentRegistryCreationRequestDto
{
    public int FirmId { get; set; }

    /// <summary> Партнёр </summary>
    public IntegrationPartners IntegrationPartner { get; set; }

    ///Номер счёта, с которого будут списаны деньги, для оплаты реестра
    public string PayerSettlementAccount { get; set; }

    ///дата исполнения реестра
    ///Если не указывать то после отправки реестра он будет ждать платежку сразу
    ///Иноче реестр будет ждать платежку с указаной даты
    public DateTime? LoadDate { get; set; }

    public List<SalaryPaymentDto> Payments { get; set; }
}