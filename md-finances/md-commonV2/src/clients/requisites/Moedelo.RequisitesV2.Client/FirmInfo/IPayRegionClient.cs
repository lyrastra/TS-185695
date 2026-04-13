using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.RequisitesV2.Dto.FirmInfo;

namespace Moedelo.RequisitesV2.Client.FirmInfo
{
    public interface IPayRegionClient : IDI
    {
        Task<FirmPayRegionDto> GetPayRegionByFirmIdAsync(int firmId);
        Task SaveAsync(int firmId, int regionId);
    }
}