using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.AspNetCore.Xss.Validation;
using Moedelo.Money.Api.Infrastructure.Validation;
using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Moedelo.Money.Resources;

namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    /// <summary>
    /// Модель для сохранения операции "Выплаты физ. лицам"
    /// </summary>
    public class PaymentToNaturalPersonsSaveDto
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
        /// Начисления
        /// </summary>
        [RequiredValue]
        public IReadOnlyCollection<EmployeePaymentsSaveDto> EmployeePayments { get; set; }

        /// <summary>
        /// Тип начисления
        /// </summary>
        [RequiredValue]
        [EnumValue(EnumType = typeof(PaymentToNaturalPersonsType))]
        public PaymentToNaturalPersonsType PaymentType { get; set; }

        /// <summary>
        /// Назначение платежа
        /// </summary>
        [ValidateXss]
        [PaymentOrderDescription]
        [DefaultValueFromResource(typeof(PaymentOrdersDescriptions), "PaymentToNaturalPersons")]
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