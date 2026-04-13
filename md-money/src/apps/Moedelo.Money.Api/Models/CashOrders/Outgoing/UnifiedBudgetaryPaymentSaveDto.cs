using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.AspNetCore.Xss.Validation;
using Moedelo.Money.Api.Infrastructure.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Moedelo.Money.Api.Models.CashOrders.Outgoing
{
    public class UnifiedBudgetaryPaymentSaveDto
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
        [CashOrderNumber]
        [RequiredValue]
        [ValidateXss]
        public string Number { get; set; }

        /// <summary>
        /// Идентификатор кассы
        /// </summary>
        [IdIntValue]
        [RequiredValue]
        public int CashId { get; set; }

        /// <summary>
        /// Сумма платежа
        /// </summary>
        [OperationSum]
        [RequiredValue]
        public decimal Sum { get; set; }

        /// <summary>
        /// Получатель платежа
        /// </summary>
        [RequiredValue]
        [ValidateXss]
        public string Recipient { get; set; }

        /// <summary>
        /// Назначение платежа
        /// </summary>
        [RequiredValue]
        [ValidateXss]
        public string Destination { get; set; }

        /// <summary>
        /// Дочерние бюджетные платежи
        /// </summary>
        [RequiredValue]
        public IReadOnlyCollection<UnifiedBudgetarySubPaymentSaveDto> SubPayments { get; set; }

        /// <summary>
        /// Признак: нужно ли проводить в бухгалтерском учете
        /// </summary>
        [DefaultValue(true)]
        public bool? ProvideInAccounting { get; set; }
    }
}
