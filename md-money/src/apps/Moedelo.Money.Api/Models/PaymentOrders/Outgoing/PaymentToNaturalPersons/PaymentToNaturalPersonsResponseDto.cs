using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.Json.Convertors;
using Moedelo.Money.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    /// <summary>
    /// Операция "Выплаты физ. лицам"
    /// </summary>
    public class PaymentToNaturalPersonsResponseDto
    {
        public long DocumentBaseId { get; set; }

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

        /// <summary>
        /// Начисления
        /// </summary>
        public IReadOnlyCollection<EmployeePaymentsResponseDto> EmployeePayments { get; set; }

        /// <summary>
        /// Тип начисления
        /// </summary>
        public PaymentToNaturalPersonsType PaymentType { get; set; }

        /// <summary>
        /// Сумма платежа
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Назначение платежа
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Признак: проведено в бухгалтерском учете
        /// </summary>
        public bool ProvideInAccounting { get; set; }

        public OperationType OperationType => OperationType.PaymentOrderOutgoingPaymentToNaturalPersons;

        /// <summary>
        /// Оплачен
        /// </summary>
        public bool IsPaid { get; set; }

        /// <summary>
        /// Только "для чтения"
        /// </summary>
        public bool IsReadOnly { get; set; }
    }
}
