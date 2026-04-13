using System;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Infrastructure;
using Moedelo.Money.Enums;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Outgoing;

/// <summary>
/// Комиссия банка: подтвердить операцию в "Массовой работе с выписками" 
/// </summary>
[OperationType(OperationType.MemorialWarrantBankFee)]
public class ConfirmBankFeeDto : IConfirmPaymentOrderDto
{
    public long DocumentBaseId { get; set; }
    public DateTime Date { get; set; }
    public string Number { get; set; }
    public decimal Sum { get; set; }
    public string Description { get; set; }
    public int SettlementAccountId { get; set; }

    /// <summary>
    /// Учитывать в СНО
    /// </summary>
    public TaxationSystemType? TaxationSystemType { get; set; }

    /// <summary>
    /// Идентификатор патента (для TaxationSystemType = Patent)
    /// </summary>
    public long? PatentId { get; set; }
}