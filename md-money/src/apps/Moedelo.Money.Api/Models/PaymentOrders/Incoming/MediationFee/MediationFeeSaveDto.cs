using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.AspNetCore.Xss.Validation;
using Moedelo.Money.Api.Infrastructure.Validation;
using Moedelo.Money.Api.Models.LinkedDocuments;
using Moedelo.Money.Api.Models.TaxPostings;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Moedelo.Money.Api.Models.PaymentOrders.Incoming.MediationFee
{
    /// <summary>
    /// Модель для сохранения операции "Посредническое вознаграждение"
    /// </summary>
    public class MediationFeeSaveDto
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
        [RequiredValue]
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
        /// Связанные первичные документы
        /// </summary>
        public IReadOnlyCollection<DocumentLinkSaveDto> Documents { get; set; }

        /// <summary>
        /// Параметры налогового учёта
        /// </summary>
        public TaxPostingsSaveDto TaxPostings { get; set; }
    }
}