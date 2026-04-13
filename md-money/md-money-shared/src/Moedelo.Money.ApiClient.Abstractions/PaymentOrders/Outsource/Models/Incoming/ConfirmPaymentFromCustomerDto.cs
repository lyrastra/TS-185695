using System;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Infrastructure;
using Moedelo.Money.Enums;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Incoming;

/// <summary>
/// Оплата от покупателя: подтвердить операцию в "Массовой работе с выписками" 
/// </summary>
[OperationType(OperationType.PaymentOrderIncomingPaymentFromCustomer)]
public class ConfirmPaymentFromCustomerDto : IConfirmPaymentOrderDto
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

    /// <summary>
    /// В том числе НДС для посредничества
    /// </summary>
    public NdsDto MediationNds { get; set; }

    /// <summary>
    /// Признак посредничества
    /// </summary>
    public bool IsMediation { get; set; }

    /// <summary>
    /// Вознаграждение посредника
    /// </summary>
    public decimal? MediationCommissionSum { get; set; }

    /// <summary>
    /// Система налогообложения
    /// </summary>
    public TaxationSystemType? TaxationSystemType { get; set; }

    /// <summary>
    /// Патент
    /// </summary>
    public long? PatentId { get; set; }
}