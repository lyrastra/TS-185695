using System.Collections.Generic;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons.Dto
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
        public int TaxationSystem { get; set; }

        /// <summary>
        /// Начисления
        /// </summary>
        public IReadOnlyCollection<ChargePaymentResponseDto> ChargePayments { get; set; }
    }
}
