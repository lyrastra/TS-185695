using Moedelo.AccountingV2.Dto.Statements;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Threading.Tasks;

namespace Moedelo.AccountingV2.Client.Statement
{
    public interface IStatementApiClient : IDI
    {
        Task<SavedStatementDto> CreateAsync(int firmId, int userId, StatementDto dto);

        Task DeleteByBaseIdAsync(int firmId, int userId, long documentBaseId);
    }
}
