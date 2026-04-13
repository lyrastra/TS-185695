using System.Collections.Generic;

namespace Moedelo.Money.Dto.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    public class EmployeePaymentsSaveDto
    {
        /// <summary>
        /// Физ. лицо
        /// </summary>
        public EmployeeSaveDto Employee { get; set; }

        /// <summary>
        /// Начисления
        /// </summary>
        public IReadOnlyCollection<ChargePaymentSaveDto> ChargePayments { get; set; }

    }
}
