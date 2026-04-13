using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.Json.Convertors;
using Moedelo.Money.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    public class UnifiedBudgetaryPaymentResponseDto
    {
        /// <summary>
        /// Дата документа
        /// </summary>
        [DateValue]
        [JsonConverter(typeof(IsoDateConverter))]
        public DateTime Date { get; set; }

        /// <summary>
        /// Номер документа
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Идентификатор расчётного счёта
        /// </summary>
        public int SettlementAccountId { get; set; }

        public BudgetaryAccountCodes AccountCode { get; set; }

        /// <summary>
        /// Идентификатор КБК
        /// </summary>
        public int KbkId { get; set; }

        /// <summary>
        /// Номер КБК (104)
        /// </summary>
        public string KbkNumber { get; set; }

        /// <summary>
        /// Реквизиты получателя
        /// </summary>
        public UnifiedBudgetaryRecipientResponseDto Recipient { get; set; }

        /// <summary>
        /// Сумма платежа (7)
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Назначение платежа (24)
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Признак: нужно ли проводить в бухгалтерском учете
        /// </summary>
        public bool ProvideInAccounting { get; set; }

        /// <summary>
        /// Признак: Оплачен
        /// </summary>
        public bool IsPaid { get; set; }

        /// <summary>
        /// УИН
        /// </summary>
        public string Uin { get; set; }

        public bool IsReadOnly { get; set; }
        
        /// <summary>
        /// Статус плательщика
        /// </summary>
        public BudgetaryPayerStatus? PayerStatus { get; set; }

        public OperationType OperationType => OperationType.PaymentOrderOutgoingUnifiedBudgetaryPayment;

        public IReadOnlyCollection<UnifiedBudgetarySubPaymentResponseDto> SubPayments { get; set; }

        public OperationState OperationState { get; set; }
    }
}
