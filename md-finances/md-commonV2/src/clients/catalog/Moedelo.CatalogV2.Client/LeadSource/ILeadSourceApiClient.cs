using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CatalogV2.Dto.LeadSource;
using Moedelo.Common.Enums.Enums.Partner;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.CatalogV2.Client.LeadSource
{
    public interface ILeadSourceApiClient : IDI
    {
        Task<List<LeadSourceDto>> GetListAsync(RegionalPartnerType? partnerType);

        Task<int> SaveOrUpdateAsync(LeadSourceDto leadSourceDto);

        Task DeleteAsync(int id);
    }
}