using Moedelo.Outsource.Dto.Employees;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Outsource.Client.Employees;

public interface IOutsourceEmployeeApiClient
{
    Task<EmployeeDto> GetAsync(int accountId, int firmId, int userId, CancellationToken cancellationToken = default);

    Task<int> InsertAsync(int accountId, int firmId, int userId, EmployeePostDto dto);

    Task<EmployeeDto[]> GetByIdsAsync(int accountId, IReadOnlyCollection<int> ids);

    Task<EmployeeDto[]> GetByUserIdsAsync(int accountId, IReadOnlyCollection<int> userIds);
}