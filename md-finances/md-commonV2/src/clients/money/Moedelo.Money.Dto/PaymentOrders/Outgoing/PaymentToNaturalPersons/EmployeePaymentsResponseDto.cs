using System.Collections.Generic;

namespace Moedelo.Money.Dto.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    public class EmployeePaymentsResponseDto
    {
        /// <summary>
        /// Физ. лицо
        /// </summary>
        public EmployeeResponseDto Employee { get; set; }

        /// <summary>
        /// Начисления
        /// </summary>
        public IReadOnlyCollection<ChargePaymentResponseDto> ChargePayments { get; set; }

    }
}
