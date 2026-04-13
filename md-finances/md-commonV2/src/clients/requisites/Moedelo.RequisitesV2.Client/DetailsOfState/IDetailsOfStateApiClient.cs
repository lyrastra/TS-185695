using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.RequisitesV2.Dto.DetailsOfState;

namespace Moedelo.RequisitesV2.Client.DetailsOfState
{
    public interface IDetailsOfStateApiClient : IDI
    {
        Task<DetailsOfStateDto> GetFssDetailsByIdAsync(int firmId, int userId, int id);
        Task<DetailsOfStateDto> GetPfrDetailsByIdAsync(int firmId, int userId, int id);
    }
}