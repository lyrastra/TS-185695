using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Docs.Dto.Upd;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Docs.Client.Upd
{
    public interface IUpdBaseInfoApiClient : IDI
    {
        Task<UpdBaseInfoDto> GetByIdAsync(int firmId, int userId, int id);

        Task<UpdBaseInfoDto> GetByBaseIdAsync(int firmId, int userId, long documentBaseId); 
    }
}