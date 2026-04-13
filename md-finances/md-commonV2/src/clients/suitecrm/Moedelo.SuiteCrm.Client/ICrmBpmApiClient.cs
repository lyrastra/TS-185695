using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.SuiteCrm.Dto.Bpm;

namespace Moedelo.SuiteCrm.Client
{
    public interface ICrmBpmApiClient : IDI
    {
        Task<List<BpmNotifyLeadInfo>> GetInfoNoStatementNoZeroNoIntegrationsAsync(IEnumerable<int> ids);
        Task<List<BpmNotifyLeadInfo>> GetInfoNoStatementAsync(IEnumerable<int> ids);
        Task ResetStatementsStatusesAsync();
        Task<List<AccountantInfoDto>> GetAccountantInfoAsync(IEnumerable<int> firmIds);
    }
}