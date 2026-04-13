using System;
using System.ComponentModel;
using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.AspNetCore.Xss.Validation;
using Moedelo.Money.Api.Infrastructure.Validation;

namespace Moedelo.Money.Api.Models.PaymentOrders.Incoming.CurrencySale
{
    /// <summary>
    /// Модель для сохранения операции "Поступление от продажи валюты"
    /// </summary>
    public class IncomingCurrencySaleSaveDto
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
        /// Идентификатор рублевого расчётного счёта
        /// </summary>
        [IdIntValue]
        [RequiredValue]
        public int SettlementAccountId { get; set; }
        
        /// <summary>
        /// Идентификатор валютного расчётного счёта (с которого производится списание)
        /// </summary>
        [IdIntValue]
        [RequiredValue]
        public int FromSettlementAccountId { get; set; }

        /// <summary>
        /// Сумма платежа в рублях
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
        /// Признак: нужно ли проводить в бухгалтерском учете
        /// </summary>
        [DefaultValue(true)]
        public bool? ProvideInAccounting { get; set; }
    }
}