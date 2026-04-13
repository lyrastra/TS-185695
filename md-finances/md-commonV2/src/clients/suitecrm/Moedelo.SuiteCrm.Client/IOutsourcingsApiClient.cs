using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.SuiteCrm.Dto.Bpm;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.SuiteCrm.Client
{
    public interface IOutsourcingsApiClient : IDI
    {
        Task<OutsourcingDto[]> GetListByClientIdsAsync(IReadOnlyCollection<int> clientIds);
    }
}
