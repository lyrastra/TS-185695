using System;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Infrastructure;
using Moedelo.Money.Enums;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Incoming;

/// <summary>
/// Входящее П/П Прочее: подтвердить операцию в "Массовой работе с выписками" 
/// </summary>
[OperationType(OperationType.PaymentOrderIncomingOther)]
public class ConfirmOtherIncomingDto : IConfirmPaymentOrderDto
{
    public long DocumentBaseId { get; set; }
    public DateTime Date { get; set; }
    public string Number { get; set; }
    public decimal Sum { get; set; }
    public string Description { get; set; }
    public int SettlementAccountId { get; set; }
    
    /// <summary>
    /// Контрагент (может быть разных типов)
    /// </summary>
    public ConfirmContractorDto Contractor { get; set; }

    /// <summary>
    /// В том числе НДС
    /// </summary>
    public NdsDto Nds { get; set; }

    /// <summary>
    /// Система налогообложения
    /// </summary>
    public TaxationSystemType? TaxationSystemType { get; set; }

    /// <summary>
    /// Целевое поступление
    /// </summary>
    public bool IsTargetIncome { get; set; }

    /// <summary>
    /// Было применено правило импорта в массовых операциях
    /// </summary>
    public bool IsOutsourceImportRuleApplied { get; set; }
}