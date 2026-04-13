using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.AspNetCore.Xss.Validation;
using Moedelo.Money.Api.Infrastructure.Validation;
using Moedelo.Money.Api.Models.LinkedDocuments;
using Moedelo.Money.Api.Models.TaxPostings;
using Moedelo.Money.Enums;
using System;
using System.ComponentModel;

namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.CurrencyOther
{
    /// <summary>
    /// Модель для сохранения операции "Прочее валютное списание"
    /// </summary>
    public class CurrencyOtherOutgoingSaveDto
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
        [ValidateXss]
        [RequiredValue]
        [PaymentOrderNumber]
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
        public OtherContractorSaveDto Contractor { get; set; }

        /// <summary>
        /// Тип контрагента
        /// </summary>
        [RequiredValue]
        [Values(ContractorType.Kontragent, ContractorType.Worker, ContractorType.Ip)]
        public ContractorType ContractorType { get; set; }

        /// <summary>
        /// Договор
        /// </summary>
        public ContractSaveDto Contract { get; set; }

        /// <summary>
        /// Сумма платежа в валюте
        /// </summary>
        [SumValue]
        [RequiredValue]
        public decimal Sum { get; set; }

        /// <summary>
        /// Сумма платежа в рублях
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
        /// Параметры налогового учёта
        /// </summary>
        public CustomTaxPostingsSaveDto TaxPostings { get; set; }

        /// <summary>
        /// Признак: нужно ли проводить в бухгалтерском учете
        /// </summary>
        [DefaultValue(true)]
        public bool? ProvideInAccounting { get; set; }

        /// <summary>
        /// Бухгалтерский учёт
        /// </summary>
        public CurrencyOutgoingCustomAccPostingDto AccountingPosting { get; set; }

        /// <summary>
        /// Признак: нужно ли зафиксировать номер платежа в нумерации
        /// </summary>
        [DefaultValue(false)]
        public bool IsSaveNumeration { get; set; }
    }
}