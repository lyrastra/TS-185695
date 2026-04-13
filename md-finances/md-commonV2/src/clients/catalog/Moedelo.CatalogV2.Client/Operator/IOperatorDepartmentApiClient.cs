using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CatalogV2.Dto.Operator;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.CatalogV2.Client.Operator
{
    public interface IOperatorDepartmentApiClient : IDI
    {
        Task<OperatorDepartmentDto> GetByIdAsync(int id);

        Task<List<OperatorDepartmentDto>> GetAllAsync();
    }
}