using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.AspNetCore.Xss.Validation;
using Moedelo.Money.Api.Infrastructure.Validation;
using Moedelo.Money.Api.Models.TaxPostings;
using System;
using System.ComponentModel;

namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.CurrencySale
{
    public class OutgoingCurrencySaleSaveDto
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
        /// Идентификатор валютного расчётного счёта
        /// </summary>
        [IdIntValue]
        [RequiredValue]
        public int SettlementAccountId { get; set; }

        /// <summary>
        /// Идентификатор рублевого расчётного счёта
        /// </summary>
        [IdIntValue]
        [RequiredValue]
        public int ToSettlementAccountId { get; set; }

        /// <summary>
        /// Сумма платежа в валюте
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
        /// Курс валюты на дату документа
        /// </summary>
        [RequiredValue]
        [SumValue(minValue: 0.0001, maxValue: 1000000000)]
        public decimal ExchangeRate { get; set; }

        /// <summary>
        /// Курсовая разница от курса ЦБ 
        /// </summary>
        [RequiredValue]
        public decimal ExchangeRateDiff { get; set; }

        /// <summary>
        /// Итог валютной операции в рублях
        /// </summary>
        [RequiredValue]
        [SumValue]
        public decimal TotalSum { get; set; }

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
        /// Признак: нужно ли зафиксировать номер платежа в нумерации
        /// </summary>
        [DefaultValue(false)]
        public bool IsSaveNumeration { get; set; }
    }
}