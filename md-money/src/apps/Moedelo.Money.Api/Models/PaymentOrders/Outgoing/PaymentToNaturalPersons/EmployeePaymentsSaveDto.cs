using Moedelo.Infrastructure.AspNetCore.Validation;
using System.Collections.Generic;

namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    public class EmployeePaymentsSaveDto
    {
        /// <summary>
        /// Физ. лицо
        /// </summary>
        [RequiredValue]
        public EmployeePaymentsEmployeeSaveDto Employee { get; set; }

        /// <summary>
        /// Начисления
        /// </summary>
        [RequiredValue]
        public IReadOnlyCollection<ChargePaymentSaveDto> ChargePayments { get; set; }

    }
}
