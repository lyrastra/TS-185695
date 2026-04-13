using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.AspNetCore.Xss.Validation;
using Moedelo.Money.Api.Infrastructure.Validation;
using Moedelo.Money.Api.Models.LinkedDocuments;
using Moedelo.Money.Api.Models.TaxPostings;
using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.BudgetaryPayment
{
    public class BudgetaryPaymentSaveDto
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
        /// Сумма платежа (7)
        /// </summary>
        [SumValue]
        [RequiredValue]
        public decimal Sum { get; set; }

        /// <summary>
        /// Назначение платежа (24)
        /// </summary>
        [ValidateXss]
        [PaymentOrderDescription]
        public string Description { get; set; }

        /// <summary>
        /// Бух.счет для типа налога/взноса
        /// </summary>
        [RequiredValue]
        [EnumValue(EnumType = typeof(BudgetaryAccountCodes))]
        public BudgetaryAccountCodes AccountCode { get; set; }

        /// <summary>
        /// Тип бюджетного платежа
        /// </summary>
        [EnumValue(EnumType = typeof(BudgetaryPaymentType), AllowNull = true)]
        public BudgetaryPaymentType? PaymentType { get; set; }

        /// <summary>
        /// Тип платежа
        /// </summary>
        [RequiredValue]
        [EnumValue(EnumType = typeof(KbkPaymentType))]
        public KbkPaymentType KbkPaymentType { get; set; }

        /// <summary>
        /// КБК
        /// </summary>
        [RequiredValue]
        public BudgetaryKbkSaveDto Kbk { get; set; }

        /// <summary>
        /// Бюджетный период
        /// </summary>
        [RequiredValue]
        public BudgetaryPeriodSaveDto Period { get; set; }

        /// <summary>
        /// Статус плательщика (101)
        /// </summary>
        [RequiredValue]
        [EnumValue(EnumType = typeof(BudgetaryPayerStatus))]
        public BudgetaryPayerStatus PayerStatus { get; set; }

        /// <summary>
        /// Основание платежа (106)
        /// </summary>
        [RequiredValue]
        [EnumValue(EnumType = typeof(BudgetaryPaymentBase))]
        public BudgetaryPaymentBase PaymentBase { get; set; }

        /// <summary>
        /// Дата требования/исполнительного документа (109)
        /// </summary>
        [ValidateXss]
        [BudgetaryDocDate]
        [DefaultValue("0")]
        public string DocumentDate { get; set; }

        /// <summary>
        /// Номер требования/исполнительного документа (108)
        /// </summary>
        [ValidateXss]
        [BudgetaryDocNumber]
        [DefaultValue("0")]
        public string DocumentNumber { get; set; }

        /// <summary>
        /// УИН (22)
        /// </summary>
        [Uin]
        [ValidateXss]
        public string Uin { get; set; }

        /// <summary>
        /// Реквизиты получателя
        /// </summary>
        [RequiredValue]
        public BudgetaryRecipientSaveDto Recipient { get; set; }

        /// <summary>
        /// Признак: нужно ли проводить в бухгалтерском учете
        /// </summary>
        [DefaultValue(true)]
        public bool? ProvideInAccounting { get; set; }

        /// <summary>
        /// Параметры налогового учёта
        /// </summary>
        public TaxPostingsSaveDto TaxPostings { get; set; }

        /// <summary>
        /// Признак: Оплачен
        /// </summary>
        [DefaultValue(false)]
        public bool IsPaid { get; set; }

        /// <summary>
        /// Идентификатор торгового объекта
        /// </summary>
        [IdIntValue]
        [DefaultValue(null)]
        public int? TradingObjectId { get; set; }

        /// <summary>
        /// Система налогообложения, в которой учитывается платеж
        /// </summary>
        [EnumValue(EnumType = typeof(TaxationSystemType), AllowNull = true)]
        [DefaultValue(null)]
        public TaxationSystemType? TaxationSystemType { get; set; }

        /// <summary>
        /// Идентификатор патента
        /// </summary>
        [IdLongValue]
        [DefaultValue(null)]
        public long? PatentId { get; set; }

        /// <summary>
        /// Связанные инвойсы на покупку (оплата НДС импортируемых товаров и услуг)
        /// </summary>
        public IReadOnlyCollection<DocumentLinkSaveDto> CurrencyInvoices { get; set; } = Array.Empty<DocumentLinkSaveDto>();

        /// <summary>
        /// Признак: нужно ли зафиксировать номер платежа в нумерации
        /// </summary>
        [DefaultValue(false)]
        public bool IsSaveNumeration { get; set; }
    }
}
