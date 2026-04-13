using System;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Infrastructure;
using Moedelo.Money.Enums;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Outgoing;

/// <summary>
/// Оплата поставщику: подтвердить операцию в "Массовой работе с выписками"
/// </summary>
[OperationType(OperationType.PaymentOrderOutgoingPaymentToSupplier)]
public class ConfirmPaymentToSupplierDto : IConfirmPaymentOrderDto
{
    public long DocumentBaseId { get; set; }
    public DateTime Date { get; set; }
    public string Number { get; set; }
    public decimal Sum { get; set; }
    public string Description { get; set; }
    public int SettlementAccountId { get; set; }
    
    /// <summary>
    /// Контрагент
    /// </summary>
    public ConfirmContractorDto Contractor { get; set; }

    /// <summary>
    /// В том числе НДС
    /// </summary>
    public NdsDto Nds { get; set; }
}