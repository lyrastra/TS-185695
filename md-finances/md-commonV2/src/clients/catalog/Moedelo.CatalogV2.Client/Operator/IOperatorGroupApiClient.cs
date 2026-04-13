using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CatalogV2.Dto.Operator;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.CatalogV2.Client.Operator
{
    public interface IOperatorGroupApiClient : IDI
    {
        Task<OperatorGroupDto> GetByIdAsync(int id);

        Task<List<OperatorGroupDto>> GetAllAsync();
    }
}