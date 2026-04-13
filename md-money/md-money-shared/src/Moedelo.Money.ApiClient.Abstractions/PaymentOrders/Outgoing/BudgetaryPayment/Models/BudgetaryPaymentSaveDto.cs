using System;
using System.Collections.Generic;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto.Providing.TaxPostings;
using Moedelo.Money.Enums;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment.Models;

public class BudgetaryPaymentSaveDto
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

    /// <summary>
    /// Сумма платежа (7)
    /// </summary>
    public decimal Sum { get; set; }

    /// <summary>
    /// Назначение платежа (24)
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Бух.счет для типа налога/взноса
    /// </summary>
    public BudgetaryAccountCodes AccountCode { get; set; }

    /// <summary>
    /// Тип бюджетного платежа
    /// </summary>
    public BudgetaryPaymentType? PaymentType { get; set; }

    /// <summary>
    /// Тип платежа
    /// </summary>
    public KbkPaymentType KbkPaymentType { get; set; }

    /// <summary>
    /// КБК
    /// </summary>
    public BudgetaryKbkSaveDto Kbk { get; set; }

    /// <summary>
    /// Бюджетный период
    /// </summary>
    public BudgetaryPeriodSaveDto Period { get; set; }

    /// <summary>
    /// Статус плательщика (101)
    /// </summary>
    public BudgetaryPayerStatus PayerStatus { get; set; }

    /// <summary>
    /// Основание платежа (106)
    /// </summary>
    public BudgetaryPaymentBase PaymentBase { get; set; }

    /// <summary>
    /// Дата требования/исполнительного документа (109)
    /// </summary>
    public string DocumentDate { get; set; }

    /// <summary>
    /// Номер требования/исполнительного документа (108)
    /// </summary>
    public string DocumentNumber { get; set; }

    /// <summary>
    /// УИН (22)
    /// </summary>
    public string Uin { get; set; }

    /// <summary>
    /// Реквизиты получателя
    /// </summary>
    public BudgetaryRecipientSaveDto Recipient { get; set; }

    /// <summary>
    /// Признак: нужно ли проводить в бухгалтерском учете
    /// </summary>
    public bool? ProvideInAccounting { get; set; }

    /// <summary>
    /// Параметры налогового учёта
    /// </summary>
    public TaxPostingsSaveDto TaxPostings { get; set; }

    /// <summary>
    /// Признак: Оплачен
    /// </summary>
    public bool IsPaid { get; set; }

    /// <summary>
    /// Идентификатор торгового объекта
    /// </summary>
    public int? TradingObjectId { get; set; }

    /// <summary>
    /// Система налогообложения, в которой учитывается платеж
    /// </summary>
    public TaxationSystemType? TaxationSystemType { get; set; }

    /// <summary>
    /// Идентификатор патента
    /// </summary>
    public long? PatentId { get; set; }

    /// <summary>
    /// Связанные инвойсы на покупку (оплата НДС импортируемых товаров и услуг)
    /// </summary>
    public IReadOnlyCollection<DocumentLinkSaveDto> CurrencyInvoices { get; set; } = Array.Empty<DocumentLinkSaveDto>();
}