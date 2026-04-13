using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.SuiteCrm.Dto.Synchronization;

namespace Moedelo.SuiteCrm.Client
{
    public interface ICrmSynchronizationApiClient : IDI
    {
        Task<SyncResultDto> SyncAccountAsync(int firmId);

        [Obsolete("Заменен на обработку фирм по списку SyncAccountsChangesAsync")]
        Task SyncAccontWithPayAsync();

        Task<List<int>> GetAccountsFirmIdsAsync();

        Task SyncAccountsChangesAsync(IReadOnlyCollection<int> firmIds);
    }
}
