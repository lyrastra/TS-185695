using Moedelo.Money.Enums;
using System.Collections.Generic;

namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    public class EmployeePaymentsResponseDto
    {
        /// <summary>
        /// Физ. лицо
        /// </summary>
        public EmployeeResponseDto Employee { get; set; }

        /// <summary>
        /// СНО физ. лица
        /// </summary>
        public TaxationSystemType TaxationSystem { get; set; }

        /// <summary>
        /// Начисления
        /// </summary>
        public IReadOnlyCollection<ChargePaymentResponseDto> ChargePayments { get; set; }

    }
}
