using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Money.Domain.Payroll;

namespace Moedelo.Money.Business.Payroll
{
    internal interface IEmployeeReader
    {
        Task<Employee> GetByIdAsync(int workerId);

        Task<Employee[]> GetByIdsAsync(IReadOnlyCollection<int> workerIds);
    }
}
