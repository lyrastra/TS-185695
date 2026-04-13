using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.ErptV2.Dto.FnsDirection;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.ErptV2.Client.FnsDirection
{
    public interface IFirmFnsDirectionApiClient : IDI
    {
        Task<List<FirmFnsDirectionCodesDto>> GetCodesAsync(IReadOnlyCollection<int> firmIds);
    }
}