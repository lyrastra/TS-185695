using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Edm.Dto.Status;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.Edm.Client.Contracts
{
    public interface IEdmStatusApiClient : IDI
    {
        Task<EdmStatusDto> GetEdmStatusAsync(int firmId, int userId);
    }
}
