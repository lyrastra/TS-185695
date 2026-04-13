using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CatalogV2.Dto.LeadSource;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.CatalogV2.Client.LeadSource
{
    public interface ILeadSourceChannelApiClient : IDI
    {
        Task<List<LeadSourceChannelDto>> GetListAsync();

        Task<int> SaveOrUpdateAsync(LeadSourceChannelDto channelDto);

        Task DeleteAsync(int id);
    }
}