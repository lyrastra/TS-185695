using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.AspNetCore.Xss.Validation;
using Moedelo.Money.Api.Infrastructure.Validation;
using Moedelo.Money.Business.Constants;

namespace Moedelo.Money.Api.Models.PaymentOrders
{
    /// <summary>
    /// Сотрудник
    /// </summary>
    public class EmployeeSaveDto
    {
        /// <summary>
        /// Идентификатор сотрудника
        /// </summary>
        [IdIntValue(Employee.IpAsEmployee, int.MaxValue)]
        [RequiredValue]
        public int Id { get; set; }

        /// <summary>
        /// ФИО
        /// </summary>
        [RequiredValue]
        [ValidateXss]
        [EmployeeName]
        public string Name { get; set; }
    }
}
