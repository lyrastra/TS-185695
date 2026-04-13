using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.AspNetCore.Xss.Validation;
using Moedelo.Money.Api.Infrastructure.Validation;
using Moedelo.Money.Api.Models.LinkedDocuments;
using Moedelo.Money.Api.Models.TaxPostings;
using System;
using System.ComponentModel;

namespace Moedelo.Money.Api.Models.PaymentOrders.Incoming.LoanReturn
{
    /// <summary>
    /// Модель для сохранения операции "Возврат займа или процентов"
    /// </summary>
    public class LoanReturnSaveDto
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
        /// В т.ч. проценты
        /// </summary>
        [SumValue(minValue: 0)]
        public decimal? LoanInterestSum { get; set; }

        /// <summary>
        /// Назначение платежа
        /// </summary>
        [ValidateXss]
        [PaymentOrderDescription]
        public string Description { get; set; }

        /// <summary>
        /// Признак: долгосрочный займ или кредит
        /// </summary>
        [DefaultValue(false)]
        public bool IsLongTermLoan { get; set; }

        /// <summary>
        /// Признак: нужно ли проводить в бухгалтерском учете
        /// </summary>
        [DefaultValue(true)]
        public bool? ProvideInAccounting { get; set; }

        /// <summary>
        /// Параметры налогового учёта
        /// </summary>
        public TaxPostingsSaveDto TaxPostings { get; set; }
    }
}