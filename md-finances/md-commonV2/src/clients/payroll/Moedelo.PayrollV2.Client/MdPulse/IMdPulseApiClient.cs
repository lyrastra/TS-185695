using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.PayrollV2.Client.MdPulse.DTO;

namespace Moedelo.PayrollV2.Client.MdPulse
{
    public interface IMdPulseApiClient:IDI
    {
        Task<List<FirmMrotStatusDto>> GetFirmsMrotStatusAsync(IEnumerable<int> firmIds, IEnumerable<string> regionCodes);
    }
}