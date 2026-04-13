using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Models.AccountBalances;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Domain.Interfaces.Business.AccountBalances
{
    public interface IAccountBalanceService : IDI
    {
        Task<List<FirmSettlementAccountBalance>> GetFirmSettlementAccountBalanceAsync(IUserContext context,
            CancellationToken cancellationToken);
    }
}