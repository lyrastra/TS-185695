using System.Threading;
using System.Threading.Tasks;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Models.BalanceMaster;

namespace Moedelo.Finances.Domain.Interfaces.Business.BalanceMaster
{
    public interface IBalanceMasterService
    {
        Task<BalanceMasterStatus> GetStatusAsync(IUserContext userContext, CancellationToken cancellationToken);

        Task<BalanceMasterStatus> GetBizStatusAsync(int firmId, int userId, CancellationToken ctx);

        Task<BalanceMasterStatus> GetAccStatusAsync(int firmId, int userId, CancellationToken ctx);
    }
}