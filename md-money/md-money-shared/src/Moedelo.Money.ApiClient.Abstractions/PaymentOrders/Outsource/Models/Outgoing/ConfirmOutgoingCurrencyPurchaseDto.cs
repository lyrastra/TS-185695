using System;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Infrastructure;
using Moedelo.Money.Enums;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Outgoing;

/// <summary>
/// Покупка валюты: подтвердить операцию в "Массовой работе с выписками" 
/// </summary>
[OperationType(OperationType.PaymentOrderOutgoingCurrencyPurchase)]
public class ConfirmOutgoingCurrencyPurchaseDto : IConfirmPaymentOrderDto
{
    public long DocumentBaseId { get; set; }
    public DateTime Date { get; set; }
    public string Number { get; set; }
    /// <summary>
    /// Сумма в рублях
    /// </summary>
    public decimal Sum { get; set; }
    public string Description { get; set; }
    public int SettlementAccountId { get; set; }

    /// <summary>
    /// Валютный счёт
    /// </summary>
    public int? ToSettlementAccountId { get; set; }
    
    /// <summary>
    /// Курс валюты на дату документа (НЕ ЦБ)
    /// </summary>
    public decimal ExchangeRate { get; set; }
}