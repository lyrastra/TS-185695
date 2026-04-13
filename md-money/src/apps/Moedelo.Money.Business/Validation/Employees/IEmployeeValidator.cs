using Moedelo.Money.Domain;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Validation
{
    internal interface IEmployeeValidator
    {
        Task ValidateAsync(int employeeId);
        Task ValidateAsync(IEmployee employee, string prefix);
    }
}
