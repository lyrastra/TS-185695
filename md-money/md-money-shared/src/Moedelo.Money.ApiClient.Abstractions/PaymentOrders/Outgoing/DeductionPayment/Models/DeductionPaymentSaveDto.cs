using System;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto;
using Moedelo.Money.Enums;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.DeductionPayment.Models;

public class DeductionPaymentSaveDto
{
    /// <summary>
    /// Идентификатор платежа
    /// </summary>
    public long DocumentBaseId { get; set; }

    /// <summary>
    /// Дата платежа
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Номер платежа
    /// </summary>
    public string Number { get; set; }

    /// <summary>
    /// Сумма платежа
    /// </summary>
    public decimal Sum { get; set; }

    /// <summary>
    /// Идентификатор р/с
    /// </summary>
    public int SettlementAccountId { get; set; }

    /// <summary>
    /// Описание платежа
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Контрагент
    /// </summary>
    public ContractorDto Contractor { get; set; }

    /// <summary>
    /// Договор
    /// </summary>
    public ContractDto Contract { get; set; }

    /// <summary>
    /// Признак "Оплачен"
    /// </summary>
    public bool IsPaid { get; set; }

    /// <summary>
    /// Признак "Бюджетный платеж"
    /// </summary>
    public bool IsBudgetaryDebt { get; set; }

    /// <summary>
    /// Очередность платежа
    /// </summary>
    public PaymentPriority PaymentPriority { get; set; }

    /// <summary>
    /// ОКТМО
    /// </summary>
    public string Oktmo { get; set; }

    /// <summary>
    /// КБК
    /// </summary>
    public string Kbk { get; set; }

    /// <summary>
    /// УИН
    /// </summary>
    public string Uin { get; set; }

    /// <summary>
    /// Номер документа сотрудника
    /// </summary>
    public string DeductionWorkerDocumentNumber { get; set; }

    /// <summary>
    /// Идентификатор сотрудника
    /// </summary>
    public int? DeductionWorkerId { get; set; }

    /// <summary>
    /// ИНН сотрудника
    /// </summary>
    public string DeductionWorkerInn { get; set; }

    /// <summary>
    /// Признак "Учитывать в БУ"
    /// </summary>
    public bool ProvideInAccounting { get; set; }

    /// <summary>
    /// Статус плательщика
    /// </summary>
    public BudgetaryPayerStatus PayerStatus { get; set; }

    /// <summary>
    /// Бухгалтерский учёт
    /// </summary>
    public DeductionCustomAccPostingDto AccountingPosting { get; set; }
}