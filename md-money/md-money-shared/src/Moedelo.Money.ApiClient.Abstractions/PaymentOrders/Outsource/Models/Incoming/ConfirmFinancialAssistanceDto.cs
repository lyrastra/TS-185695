using System;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Infrastructure;
using Moedelo.Money.Enums;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Incoming;

/// <summary>
/// Финансовая помощь от учредителя: подтвердить операцию в "Массовой работе с выписками" 
/// </summary>
[OperationType(OperationType.PaymentOrderIncomingFinancialAssistance)]
public class ConfirmFinancialAssistanceDto : IConfirmPaymentOrderDto
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
}