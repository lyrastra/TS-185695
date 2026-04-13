using System;
using System.ComponentModel;
using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.AspNetCore.Xss.Validation;
using Moedelo.Money.Api.Infrastructure.Validation;
using Moedelo.Money.Api.Models.TaxPostings;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Api.Models.PaymentOrders.Incoming.AccrualOfInterest
{
    /// <summary>
    /// Модель для сохранения операции "Начисление процентов от банка"
    /// </summary>
    public class AccrualOfInterestSaveDto
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
        [PaymentOrderNumber]
        [ValidateXss]
        [RequiredValue]
        public string Number { get; set; }

        /// <summary>
        /// Идентификатор расчётного счёта
        /// </summary>
        [IdIntValue]
        [RequiredValue]
        public int SettlementAccountId { get; set; }

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
        /// Учитывать в СНО
        /// УСН = 1
        /// ОСНО = 2
        /// ЕНВД = 3
        /// УСН + ЕНВД = 4
        /// ОСНО + ЕНВД = 5
        /// </summary>
        [EnumValue(EnumType = typeof(TaxationSystemType), AllowNull = true)]
        [Values(Enums.TaxationSystemType.Usn, Enums.TaxationSystemType.Osno, Enums.TaxationSystemType.Envd, Enums.TaxationSystemType.UsnAndEnvd, Enums.TaxationSystemType.OsnoAndEnvd)]
        [DefaultValue(null)]
        public TaxationSystemType? TaxationSystemType { get; set; }

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