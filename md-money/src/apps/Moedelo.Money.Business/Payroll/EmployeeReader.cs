using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Money.Domain.Payroll;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker;

namespace Moedelo.Money.Business.Payroll
{
    [InjectAsSingleton(typeof(IEmployeeReader))]
    internal sealed class EmployeeReader : IEmployeeReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IEmployeesApiClient client;

        public EmployeeReader(
            IExecutionInfoContextAccessor contextAccessor,
            IEmployeesApiClient client)
        {
            this.contextAccessor = contextAccessor;
            this.client = client;
        }

        public async Task<Employee> GetByIdAsync(int workerId)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var dto = await client.GetByIdAsync(context.FirmId, context.UserId, workerId).ConfigureAwait(false);
            return dto != null
               ? Map(dto)
               : null;
        }

        public async Task<Employee[]> GetByIdsAsync(IReadOnlyCollection<int> workerIds)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var dtos = await client.GetByIdsAsync(context.FirmId, context.UserId, workerIds).ConfigureAwait(false);
            return dtos.Select(Map).ToArray();
        }

        private static Employee Map(WorkerDto dto)
        {
            return new Employee
            {
                Id = dto.Id,
                SubcontoId = dto.SubcontoId,
                Name = dto.FullName,
                IsNotStaff = dto.IsNotStaff
            };
        }
    }
}
