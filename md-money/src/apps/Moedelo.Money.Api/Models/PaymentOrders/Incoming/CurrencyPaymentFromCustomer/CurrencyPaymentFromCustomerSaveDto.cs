using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.AspNetCore.Xss.Validation;
using Moedelo.Money.Api.Infrastructure.Validation;
using Moedelo.Money.Api.Models.LinkedDocuments;
using Moedelo.Money.Api.Models.TaxPostings;
using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Moedelo.Money.Api.Models.PaymentOrders.Incoming.CurrencyPaymentFromCustomer
{
    /// <summary>
    /// Модель для сохранения операции "Валютная оплата от покупателя"
    /// </summary>
    public class CurrencyPaymentFromCustomerSaveDto
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
        [PaymentOrderNumber]
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
        /// Итоговая сумма платежа
        /// </summary>
        [SumValue]
        [RequiredValue]
        public decimal TotalSum { get; set; }

        /// <summary>
        /// Назначение платежа
        /// </summary>
        [ValidateXss]
        [PaymentOrderDescription]
        public string Description { get; set; }

        /// <summary>
        /// НДС
        /// </summary>
        public NdsSaveDto Nds { get; set; }

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
        /// Связанные с платежом документы
        /// </summary>
        public IReadOnlyCollection<DocumentLinkSaveDto> Documents { get; set; } = Array.Empty<DocumentLinkSaveDto>();

        /// <summary>
        /// Идентификатор патента
        /// </summary>
        [IdLongValue]
        [DefaultValue(null)]
        public long? PatentId { get; set; }

        /// <summary>
        /// Учитывать в СНО
        /// УСН = 1
        /// ОСНО = 2
        /// ЕНВД = 3
        /// УСН + ЕНВД = 4
        /// ОСНО + ЕНВД = 5
        /// Патент = 6
        /// </summary>
        [DefaultValue(null)]
        [EnumValue(EnumType = typeof(TaxationSystemType), AllowNull = true)]
        public TaxationSystemType? TaxationSystemType { get; set; }
    }
}