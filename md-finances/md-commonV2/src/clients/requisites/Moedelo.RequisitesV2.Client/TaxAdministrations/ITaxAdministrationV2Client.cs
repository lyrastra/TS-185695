using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.RequisitesV2.Dto.TaxAdministrations;

namespace Moedelo.RequisitesV2.Client.TaxAdministrations
{
    public interface ITaxAdministrationV2Client : IDI
    {
        Task<TaxAdministrationV2Dto> GetForFirmAsync(int firmId, int userId);

        Task<TaxAdministrationV2Dto> GetByCodeAsync(int firmId, int userId, string code);
        
        Task<TaxAdministrationV2Dto> GetByIdAsync(int firmId, int userId, int id);
    }
}