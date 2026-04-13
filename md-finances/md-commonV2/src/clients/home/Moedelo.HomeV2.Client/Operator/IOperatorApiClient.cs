using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.HomeV2.Dto.Operator;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.HomeV2.Client.Operator
{
    public interface IOperatorApiClient : IDI
    {
        Task<int> GetOperatorIdByCodeAsync(string code);

        Task<OperatorDto> GetOperatorByIdAsync(int id);

        Task<OperatorDto> GetOperatorByUserIdAsync(int userId);

        Task<List<OperatorDto>> GetOperatorsByIdsAsync(IReadOnlyCollection<int> operatorIds);

        Task<int> GetUserIdByOperatorIdAsync(int operatorId);

        Task<List<OperatorDto>> GetOperatorsByEmailAsync(string email);

        Task<List<OperatorDto>> GetOperatorsByEmailAsync(IReadOnlyCollection<string> emails);

        Task<List<OperatorDto>> GetOperatorsListAsync(OperatorRequestDto operatorRequestDto);

        Task DeleteOperatorAsync(int id);

        Task<int> SaveOperatorAsync(OperatorDto oper);
    }
}
