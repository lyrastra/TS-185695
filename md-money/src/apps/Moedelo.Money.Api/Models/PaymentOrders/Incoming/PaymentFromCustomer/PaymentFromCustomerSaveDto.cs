using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.AspNetCore.Xss.Validation;
using Moedelo.Money.Api.Infrastructure.Validation;
using Moedelo.Money.Api.Models.LinkedDocuments;
using Moedelo.Money.Api.Models.TaxPostings;
using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Moedelo.Money.Api.Models.PaymentOrders.Incoming.PaymentFromCustomer
{
    /// <summary>
    /// Модель для сохранения операции "Оплата от покупателя"
    /// </summary>
    public class PaymentFromCustomerSaveDto
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
        /// НДС для посредничества
        /// </summary>
        public NdsSaveDto MediationNds { get; set; }

        /// <summary>
        /// Посредничество (УСН)
        /// </summary>
        public MediationSaveDto Mediation { get; set; }

        /// <summary>
        /// Признак: основной контрагент
        /// </summary>
        [DefaultValue(true)]
        public bool? IsMainContractor { get; set; }

        /// <summary>
        /// Признак: нужно ли проводить в бухгалтерском учете
        /// </summary>
        [DefaultValue(true)]
        public bool? ProvideInAccounting { get; set; }

        /// <summary>
        /// Связанные счета
        /// </summary>
        public IReadOnlyCollection<BillLinkSaveDto> Bills { get; set; } = Array.Empty<BillLinkSaveDto>();

        /// <summary>
        /// Связанные первичные документы
        /// </summary>
        public IReadOnlyCollection<DocumentLinkSaveDto> Documents { get; set; } = Array.Empty<DocumentLinkSaveDto>();

        /// <summary>
        /// Резерв (данная величина уменьшает сумму, покрываемую первичными документами)
        /// </summary>
        [PositiveNumber(AllowNull = true)]
        public decimal? ReserveSum { get; set; }

        /// <summary>
        /// Параметры налогового учёта
        /// </summary>
        public TaxPostingsSaveDto TaxPostings { get; set; }

        /// <summary>
        /// Учитывать в СНО
        /// УСН = 1
        /// ОСНО = 2
        /// ЕНВД = 3
        /// УСН + ЕНВД = 4
        /// ОСНО + ЕНВД = 5
        /// Патент = 6
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
    }
}