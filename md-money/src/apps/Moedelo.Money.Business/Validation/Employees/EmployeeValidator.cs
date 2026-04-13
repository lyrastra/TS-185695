using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Constants;
using Moedelo.Money.Business.Payroll;
using Moedelo.Money.Domain;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Validation
{
    [InjectAsSingleton(typeof(IEmployeeValidator))]
    internal sealed class EmployeeValidator : IEmployeeValidator
    {
        private readonly IEmployeeReader employeeReader;

        public EmployeeValidator(IEmployeeReader employeeReader)
        {
            this.employeeReader = employeeReader;
        }

        public Task ValidateAsync(int employeeId)
        {
            return ValidateBaseAsync(employeeId, "EmployeeId");
        }

        public async Task ValidateAsync(IEmployee employee, string name)
        {
            if (employee.Id == Employee.IpAsEmployee)
            {
                // Специальный случай - ИП выбрал сам себя в качестве сотрудника
                return;
            }

            if (employee.Id == 0)
            {
                throw new BusinessValidationException(name, $"Не указан ид {employee.Id} сотрудника");
            }

            await ValidateBaseAsync(employee.Id, name);
            EmployeeNameValidator.Validate256(employee.Name, name);
        }

        public async Task ValidateBaseAsync(int employeeId, string name)
        {
            if (employeeId == 0)
            {
                throw new BusinessValidationException(name, $"Не указан ид {employeeId} сотрудника");
            }

            var employee = await employeeReader.GetByIdAsync(employeeId);
            if (employee == null)
            {
                throw new BusinessValidationException(name, $"Не найден сотрудник с ид {employeeId}");
            }

            if (employee.SubcontoId == null)
            {
                throw new BusinessValidationException(name, $"Отсутствует субконто сотрудника с ид {employeeId}");
            }
        }
    }
}
