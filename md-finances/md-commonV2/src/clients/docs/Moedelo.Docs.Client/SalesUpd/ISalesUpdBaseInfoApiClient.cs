using System.Threading.Tasks;
using Moedelo.Docs.Dto.SalesUpd;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Docs.Client.SalesUpd
{
    public interface ISalesUpdBaseInfoApiClient : IDI
    {
        Task<SalesUpdBaseInfoDto> GetByIdAsync(int firmId, int userId, int id);
    }
}