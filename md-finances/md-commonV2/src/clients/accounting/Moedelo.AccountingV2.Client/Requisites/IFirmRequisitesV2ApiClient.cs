using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.Requsites;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccountingV2.Client.Requisites
{
    public interface IFirmRequisitesV2ApiClient : IDI
    {
        Task<FirmRequisitesV2Dto> GetUserContextRequisitesAsync(int firmId, int userId);
    }
}