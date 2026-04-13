using System;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Infrastructure;
using Moedelo.Money.Enums;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Outgoing;

/// <summary>
/// Перевод на другой счет: подтвердить операцию в "Массовой работе с выписками"
/// </summary>
[OperationType(OperationType.PaymentOrderOutgoingTransferToAccount)]
public class ConfirmTransferToAccountDto : IConfirmPaymentOrderDto
{
    public long DocumentBaseId { get; set; }
    public DateTime Date { get; set; }
    public string Number { get; set; }
    public decimal Sum { get; set; }
    public string Description { get; set; }
    public int SettlementAccountId { get; set; }
    public int? ToSettlementAccountId { get; set; }
}