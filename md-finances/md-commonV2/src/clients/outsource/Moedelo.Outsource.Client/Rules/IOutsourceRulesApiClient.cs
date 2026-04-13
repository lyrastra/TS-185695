using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Outsource.Client.Rules;

public interface IOutsourceRulesApiClient
{
    Task<List<string>> GetRulesListAsync(int accountId, int roleId, int firmId, int userId, CancellationToken cancellationToken = default);
}