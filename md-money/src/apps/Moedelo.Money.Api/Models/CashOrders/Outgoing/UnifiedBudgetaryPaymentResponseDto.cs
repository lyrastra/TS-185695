using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.Json.Convertors;
using Moedelo.Money.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Api.Models.CashOrders.Outgoing
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
        /// Идентификатор кассы
        /// </summary>
        public long CashId { get; set; }

        /// <summary>
        /// Сумма платежа
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Получатель платежа
        /// </summary>
        public string Recipient { get; set; }

        /// <summary>
        /// Назначение платежа
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// Дочерние бюджетные платежи
        /// </summary>
        public IReadOnlyCollection<UnifiedBudgetarySubPaymentResponseDto> SubPayments { get; set; }

        /// <summary>
        /// Признак: нужно ли проводить в бухгалтерском учете
        /// </summary>
        public bool ProvideInAccounting { get; set; }

        public bool IsReadOnly { get; set; }

        public OperationType OperationType => OperationType.CashOrderOutgoingUnifiedBudgetaryPayment;
    }
}
