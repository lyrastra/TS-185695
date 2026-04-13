using System;
using System.Collections.Generic;
using Moedelo.Money.Enums;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment.Dto;

public class UnifiedBudgetaryPaymentSaveDto
{
    /// <summary>
    /// Базовый идентификатор документа
    /// </summary>
    public long DocumentBaseId { get; set; }

    /// <summary>
    /// Дата документа
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Номер документа
    /// </summary>
    public string Number { get; set; }

    /// <summary>
    /// Идентификатор расчётного счёта
    /// </summary>
    public int SettlementAccountId { get; set; }

    /// <summary>
    /// Сумма платежа (7)
    /// </summary>
    public decimal Sum { get; set; }

    ///// <summary>
    ///// Назначение платежа (24) // теперь формируется в деньгах на основе переданных SubPayments
    ///// </summary>
    //public string Description { get; set; }

    /// <summary>
    /// Реквизиты получателя
    /// </summary>
    public UnifiedBudgetaryRecipientDto Recipient { get; set; }

    /// <summary>
    /// Признак: нужно ли проводить в бухгалтерском учете
    /// </summary>
    public bool? ProvideInAccounting { get; set; }

    /// <summary>
    /// Признак: Оплачен
    /// </summary>
    public bool IsPaid { get; set; }

    /// <summary>
    /// Статус плательщика (101)
    /// </summary>
    public BudgetaryPayerStatus PayerStatus { get; set; }

    public IReadOnlyCollection<UnifiedBudgetarySubPaymentSaveDto> SubPayments { get; set; }
}