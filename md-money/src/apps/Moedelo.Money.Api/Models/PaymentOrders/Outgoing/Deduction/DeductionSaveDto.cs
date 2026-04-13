using System;
using System.ComponentModel;
using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.AspNetCore.Xss.Validation;
using Moedelo.Money.Api.Infrastructure.Validation;
using Moedelo.Money.Api.Models.LinkedDocuments;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.Deduction
{
    /// <summary>
    /// Модель для сохранения операции "Выплаты удержаний"
    /// </summary>
    public class DeductionSaveDto
    {
        /// <summary>
        /// Дата документа
        /// </summary>
        [DateValue]
        [RequiredValue]
        public DateTime Date { get; set; }

        /// <summary>
        /// Номер документа
        /// </summary>
        [RequiredValue]
        [ValidateXss]
        public string Number { get; set; }

        /// <summary>
        /// Идентификатор расчётного счёта
        /// </summary>
        [IdIntValue]
        [RequiredValue]
        public int SettlementAccountId { get; set; }

        /// <summary>
        /// Контрагент
        /// </summary>
        [RequiredValue]
        public ContractorSaveDto Contractor { get; set; }

        /// <summary>
        /// Договор
        /// </summary>
        public ContractSaveDto Contract { get; set; }

        /// <summary>
        /// Сумма платежа
        /// </summary>
        [SumValue]
        [RequiredValue]
        public decimal Sum { get; set; }

        /// <summary>
        /// Назначение платежа
        /// </summary>
        [ValidateXss]
        [RequiredValue]
        [PaymentOrderDescription]
        public string Description { get; set; }

        /// <summary>
        /// Оплачен
        /// </summary>
        [DefaultValue(false)]
        public bool IsPaid { get; set; }

        /// <summary>
        /// Взыскивается долг перед бюджетом
        /// </summary>
        [DefaultValue(false)]
        public bool IsBudgetaryDebt { get; set; }

        /// <summary>
        /// Очередность платежа
        /// </summary>
        [EnumValue(EnumType = typeof(PaymentPriority))]
        public PaymentPriority PaymentPriority { get; set; }

        /// <summary>
        /// ОКТМО
        /// </summary>
        [ValidateXss]
        [Oktmo]
        public string Oktmo { get; set; }

        /// <summary>
        /// Статус плательщика (101)
        /// </summary>
        [RequiredValue]
        [EnumValue(EnumType = typeof(BudgetaryPayerStatus))]
        public BudgetaryPayerStatus PayerStatus { get; set; }

        /// <summary>
        /// Код бюджетной классификации (КБК) (104)
        /// </summary>
        [ValidateXss]
        [Kbk]
        public string Kbk { get; set; }

        /// <summary>
        /// Номер документа (108)
        /// </summary>
        [ValidateXss]
        [BudgetaryDocNumber]
        public string DeductionWorkerDocumentNumber { get; set; }

        /// <summary>
        /// УИН (22)
        /// </summary>
        [ValidateXss]
        [Uin]
        public string Uin { get; set; }

        /// <summary>
        /// Сотрудник, у которого удерживается сумма
        /// </summary>
        [IdIntValue]
        public int? DeductionWorkerId { get; set; }

        [ValidateXss]
        [Inn]
        public string DeductionWorkerInn { get; set; }
        /// <summary>
        /// Признак: нужно ли проводить в бухгалтерском учете
        /// </summary>
        [DefaultValue(true)]
        public bool? ProvideInAccounting { get; set; }

        /// <summary>
        /// Бухгалтерский учёт
        /// </summary>
        public DeductionCustomAccPostingDto AccountingPosting { get; set; }

        /// <summary>
        /// Признак: нужно ли зафиксировать номер платежа в нумерации
        /// </summary>
        [DefaultValue(false)]
        public bool IsSaveNumeration { get; set; }
    }
}