using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons.Dto
{
    public class PaymentToNaturalPersonsResponseDto
    {
        /// <summary>
        /// Базовый идентификатор документа
        /// </summary>
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public int SettlementAccountId { get; set; }

        public IReadOnlyCollection<EmployeePaymentsResponseDto> EmployeePayments { get; set; }

        public PaymentToNaturalPersonsType PaymentType { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public bool ProvideInAccounting { get; set; }

        public bool IsPaid { get; set; }
    }
}
