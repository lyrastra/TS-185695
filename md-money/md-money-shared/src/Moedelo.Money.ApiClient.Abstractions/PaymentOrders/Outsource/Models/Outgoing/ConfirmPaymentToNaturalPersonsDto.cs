using System;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Infrastructure;
using Moedelo.Money.Enums;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Outgoing;

/// <summary>
/// Выплаты физ. лицам: подтвердить операцию в "Массовой работе с выписками"
/// </summary>
[OperationType(OperationType.PaymentOrderOutgoingPaymentToNaturalPersons)]
public class ConfirmPaymentToNaturalPersonsDto : IConfirmPaymentOrderDto
{
    public long DocumentBaseId { get; set; }
    public DateTime Date { get; set; }
    public string Number { get; set; }
    public decimal Sum { get; set; }
    public string Description { get; set; }
    public int SettlementAccountId { get; set; }
    /// <summary>
    /// Получатель (сотрудник)
    /// </summary>
    public ConfirmContractorDto Employee { get; set; }
}