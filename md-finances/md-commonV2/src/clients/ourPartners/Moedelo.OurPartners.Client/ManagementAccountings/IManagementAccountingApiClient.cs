using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.OurPartners.Dto.ManagementAccountings;

namespace Moedelo.OurPartners.Client.ManagementAccountings
{
    public interface IManagementAccountingApiClient : IDI
    {
        Task<ManagementAccountingInfoDto> GetInfo(int firmId, int userId);
    }
}