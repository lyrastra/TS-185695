using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Manufacturing.Dto;

namespace Moedelo.Manufacturing.Client
{
    public interface IManufacturingSettingsClient : IDI
    {
        Task<ManufacturingSettingDto> GetAsync(int firmId, int userId);
    }
}