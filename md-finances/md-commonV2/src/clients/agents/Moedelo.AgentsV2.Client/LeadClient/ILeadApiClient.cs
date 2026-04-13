using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AgentsV2.Client.Dto.Lead;
using Moedelo.AgentsV2.Dto.Leads;
using Moedelo.Common.Enums.Enums.Agents;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.AgentsV2.Client.LeadClient
{
    public interface ILeadApiClient : IDI
    {
        Task<LeadStatusInfoDto> GetLeadStatusInfo(string loginQuery);

        Task SetLeadStatus(int userId, LeadStatus status);

        Task AddNewPartnerLeadAsync(AddNewPartnerLeadDto dto);

        Task<PartnerLeadsResponseDto> GetPartnerLeadsAsync(PartnerLeadRequestDto dto);
    }
}
