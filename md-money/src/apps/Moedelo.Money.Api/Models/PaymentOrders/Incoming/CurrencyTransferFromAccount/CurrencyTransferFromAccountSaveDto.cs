using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.AspNetCore.Xss.Validation;
using Moedelo.Money.Api.Infrastructure.Validation;
using System;
using System.ComponentModel;

namespace Moedelo.Money.Api.Models.PaymentOrders.Incoming.CurrencyTransferFromAccount
{
    /// <summary>
    /// Модель для сохранения операции "Перевод с валютного счета"
    /// </summary>
    public class CurrencyTransferFromAccountSaveDto
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
        /// Идентификатор валютного расчётного счёта, на который был осуществлен перевод
        /// </summary>
        [IdIntValue]
        [RequiredValue]
        public int SettlementAccountId { get; set; }

        /// <summary>
        /// Идентификатор валютного расчётного счёта, с которого был осуществлен перевод
        /// </summary>
        [IdIntValue]
        [RequiredValue]
        public int FromSettlementAccountId { get; set; }

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
        /// Проводки в БУ
        /// </summary>
        [DefaultValue(true)]
        public bool? ProvideInAccounting { get; set; }
    }
}