using System;
using System.Collections.Generic;
using Moedelo.Money.Enums;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment.Dto;

public class UnifiedBudgetaryPaymentDto
{
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

    public BudgetaryAccountCodes AccountCode { get; set; }

    /// <summary>
    /// Идентификатор КБК
    /// </summary>
    public int KbkId { get; set; }

    /// <summary>
    /// Номер КБК (104)
    /// </summary>
    public string KbkNumber { get; set; }

    /// <summary>
    /// Реквизиты получателя
    /// </summary>
    public UnifiedBudgetaryRecipientDto Recipient { get; set; }

    /// <summary>
    /// Сумма платежа (7)
    /// </summary>
    public decimal Sum { get; set; }

    /// <summary>
    /// Назначение платежа (24)
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Признак: нужно ли проводить в бухгалтерском учете
    /// </summary>
    public bool ProvideInAccounting { get; set; }

    /// <summary>
    /// Признак: Оплачен
    /// </summary>
    public bool IsPaid { get; set; }

    public bool IsReadOnly { get; set; }

    /// <summary>
    /// Статус плательщика
    /// </summary>
    public BudgetaryPayerStatus? PayerStatus { get; set; }

    public IReadOnlyCollection<UnifiedBudgetarySubPaymentDto> SubPayments { get; set; }

    public OperationState OperationState { get; set; }
}