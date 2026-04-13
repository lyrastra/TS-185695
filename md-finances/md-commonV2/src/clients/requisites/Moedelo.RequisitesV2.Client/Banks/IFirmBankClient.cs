using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.RequisitesV2.Dto.FirmBanks;

namespace Moedelo.RequisitesV2.Client.Banks
{
    public interface IFirmBankClient : IDI
    {
        Task<List<FirmBanksDto>> GetAsync(IReadOnlyCollection<int> firmIds);
    }
}