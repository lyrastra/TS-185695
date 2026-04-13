using System.Collections.Generic;
using Moedelo.HomeV2.Dto.ClientSource;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Threading.Tasks;

namespace Moedelo.HomeV2.Client.ClientSource
{
    public interface IClientSourceApiClient : IDI
    {
        Task<List<ClientSourceDto>> GetAllSourceForPromoCodeAsync();

        Task<int> SaveClientSourceAsync(ClientSourceDto requestDto);

        Task<bool> CheckExistsClientSourceByNameAsync(string name);

        Task DeleteClientSourceAsync(int id);
    }
}
