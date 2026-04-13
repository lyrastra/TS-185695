using System;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Infrastructure;
using Moedelo.Money.Enums;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Incoming;

/// <summary>
/// Входящее П/П Инкассированные денежные средства: подтвердить операцию в "Массовой работе с выписками" 
/// </summary>
[OperationType(OperationType.MemorialWarrantTransferFromCashCollection)]
public class ConfirmTransferFromCashCollectionDto : IConfirmPaymentOrderDto
{
    public long DocumentBaseId { get; set; }
    public DateTime Date { get; set; }
    public string Number { get; set; }
    public decimal Sum { get; set; }
    public string Description { get; set; }
    public int SettlementAccountId { get; set; }
}