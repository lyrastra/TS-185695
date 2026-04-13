using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.AspNetCore.Xss.Validation;
using Moedelo.Money.Api.Infrastructure.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
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
        /// Сумма платежа (7)
        /// </summary>
        [OperationSum]
        [RequiredValue]
        public decimal Sum { get; set; }

        /// <summary>
        /// Назначение платежа (24)
        /// </summary>
        [ValidateXss]
        [PaymentOrderDescription]
        public string Description { get; set; }

        /// <summary>
        /// Реквизиты получателя
        /// </summary>
        [RequiredValue]
        public UnifiedBudgetaryRecipientSaveDto Recipient { get; set; }

        /// <summary>
        /// Признак: нужно ли проводить в бухгалтерском учете
        /// </summary>
        [DefaultValue(true)]
        public bool? ProvideInAccounting { get; set; }

        /// <summary>
        /// Признак: Оплачен
        /// </summary>
        [DefaultValue(false)]
        public bool IsPaid { get; set; }

        /// <summary>
        /// УИН
        /// </summary>
        [ValidateXss]
        [Uin]
        public string Uin { get; set; }

        /// <summary>
        /// Статус плательщика (101)
        /// </summary>
        [EnumValue(EnumType = typeof(BudgetaryPayerStatus))]
        public BudgetaryPayerStatus PayerStatus { get; set; }

        [RequiredValue]
        public IReadOnlyCollection<UnifiedBudgetarySubPaymentSaveDto> SubPayments { get; set; }

        /// <summary>
        /// Признак: нужно ли зафиксировать номер платежа в нумерации
        /// </summary>
        [DefaultValue(false)]
        public bool IsSaveNumeration { get; set; }
    }
}
