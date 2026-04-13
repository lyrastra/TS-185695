using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.AspNetCore.Xss.Validation;
using Moedelo.Money.Api.Infrastructure.Validation;
using System;
using System.ComponentModel;
using Moedelo.Money.Resources;

namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.TransferToAccount
{
    /// <summary>
    /// Модель для сохранения операции "Перевод на другой счет"
    /// </summary>
    public class TransferToAccountSaveDto
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
        /// Идентификатор расчётного счёта с которого был осуществлен перевод
        /// </summary>
        [IdIntValue]
        [RequiredValue]
        public int SettlementAccountId { get; set; }

        /// <summary>
        /// Идентификатор расчётного счёта на который был осуществлен перевод
        /// </summary>
        [IdIntValue]
        [RequiredValue]
        public int ToSettlementAccountId { get; set; }

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
        [DefaultValueFromResource(typeof(PaymentOrdersDescriptions), "TransferToAccount")]
        public string Description { get; set; }

        /// <summary>
        /// Признак: нужно ли проводить в бухгалтерском учете
        /// </summary>
        [DefaultValue(true)]
        public bool? ProvideInAccounting { get; set; }

        /// <summary>
        /// Оплачен
        /// </summary>
        [DefaultValue(false)]
        public bool IsPaid { get; set; }

        /// <summary>
        /// Признак: нужно ли зафиксировать номер платежа в нумерации
        /// </summary>
        [DefaultValue(false)]
        public bool IsSaveNumeration { get; set; }
    }
}