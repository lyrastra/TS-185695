using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.AspNetCore.Xss.Validation;
using Moedelo.Money.Api.Infrastructure.Validation;
using Moedelo.Money.Api.Models.AccPosting;
using Moedelo.Money.Api.Models.LinkedDocuments;
using Moedelo.Money.Api.Models.TaxPostings;
using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Moedelo.Money.Api.Models.PaymentOrders.Incoming.Other
{
    /// <summary>
    /// Модель для сохранения операции "Прочее поступление"
    /// </summary>
    public class OtherIncomingSaveDto
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
        /// Контрагент/Сотрудник/ИП
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
        /// Признак: нужно ли проводить в бухгалтерском учете
        /// </summary>
        [DefaultValue(true)]
        public bool? ProvideInAccounting { get; set; }

        /// <summary>
        /// Связанные счета
        /// </summary>
        public IReadOnlyCollection<BillLinkSaveDto> Bills { get; set; }

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

        /// <summary>
        /// Идентификатор патента
        /// </summary>
        [IdLongValue]
        [DefaultValue(null)]
        public long? PatentId { get; set; }

        /// <summary>
        /// Параметры налогового учёта
        /// </summary>
        public CustomTaxPostingsSaveDto TaxPostings { get; set; }

        /// <summary>
        /// Бухгалтерский учёт
        /// </summary>
        public IncomingCustomAccPostingDto AccountingPosting { get; set; }

        /// <summary>
        /// Не удаляемая атоматически
        /// </summary>
        [DefaultValue(false)]
        public bool NoAutoDeleteOperation { get; set; }

        /// <summary>
        /// Целевое поступление
        /// </summary>
        [DefaultValue(false)]
        public bool? IsTargetIncome { get; set; }
    }
}