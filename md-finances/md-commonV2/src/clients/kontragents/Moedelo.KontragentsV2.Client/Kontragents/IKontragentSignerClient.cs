using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.KontragentsV2.Dto;

namespace Moedelo.KontragentsV2.Client.Kontragents
{
    public interface IKontragentSignerClient : IDI
    {
        Task<KontragentSignerDto> GetAsync(int firmId, int userId, int kontragentId);

        Task<KontragentSignerDto> GetByKontragentAsync(int firmId, int userId, int kontragentId);

        Task SaveAsync(int firmId, int userId, int kontragentId, KontragentSignerDto dto);
    }
}
