using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.AspNetCore.Xss.Validation;
using Moedelo.Money.Api.Infrastructure.Validation;
using Moedelo.Money.Api.Models.LinkedDocuments;
using System;
using System.ComponentModel;
using Moedelo.Money.Resources;

namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.WithdrawalFromAccount
{
    /// <summary>
    /// Модель для сохранения операции "Снятие с р/сч"
    /// </summary>
    public class WithdrawalFromAccountSaveDto
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
        [ValidateXss]
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
        [DefaultValueFromResource(typeof(PaymentOrdersDescriptions), "WithdrawalFromAccount")]
        public string Description { get; set; }

        /// <summary>
        /// Расходно-кассовый ордер
        /// </summary>
        public CashOrderSaveDto CashOrder { get; set; }

        /// <summary>
        /// Признак: нужно ли проводить в бухгалтерском учете
        /// </summary>
        [DefaultValue(true)]
        public bool? ProvideInAccounting { get; set; }

        /// <summary>
        /// Признак: нужно ли зафиксировать номер платежа в нумерации
        /// </summary>
        [DefaultValue(false)]
        public bool IsSaveNumeration { get; set; }
    }
}