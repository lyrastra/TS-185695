using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.AspNetCore.Xss.Validation;
using Moedelo.Money.Api.Infrastructure.Validation;

namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    public class EmployeePaymentsEmployeeSaveDto
    {
        /// <summary>
        /// Идентификатор сотрудника
        /// </summary>
        [IdIntValue]
        [RequiredValue]
        public int Id { get; set; }

        /// <summary>
        /// ФИО
        /// </summary>
        [ValidateXss]
        [EmployeeName]
        public string Name { get; set; }
    }
}
