using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.SuiteCrm.Dto.Bpm;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.SuiteCrm.Client
{
    public interface IClientApiClient : IDI
    {
        Task<ClientDto[]> GetByFirmIdsAsync(IReadOnlyCollection<int> firmIds);
    }
}
