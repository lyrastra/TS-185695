using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountingStatements.ApiClient.Abstractions.Dtos;

namespace Moedelo.AccountingStatements.ApiClient.Abstractions
{
    public interface IAccountingStatementApiClient
    {
        Task<List<AccountingStatementSimpleDto>> GetByBaseIds(int firmId, int userId, IReadOnlyCollection<long> baseIds);
        Task<long> CreateAccountingStatementAsync(int firmId, int userId, AccountingStatementDto data);
        Task<AccountingStatementSaveDto> CreateAccountingStatementWithBaseIdAsync(int firmId, int userId, AccountingStatementDto data);
        Task DeleteAccountingStatement(int firmId, int userId, long statementId);
        Task DeleteByBaseIdAsync(int firmId, int userId, long documentBaseId);
    }
}