using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.OutSystemsIntegrationV2.Dto;
using Moedelo.OutSystemsIntegrationV2.Dto.CaseBookApi;
using Moedelo.OutSystemsIntegrationV2.Dto.CaseBookApi.Executory;
using Moedelo.OutSystemsIntegrationV2.Dto.CaseBookApi.Pledges;

namespace Moedelo.OutSystemsIntegrationV2.Client.CaseBookApi.V3
{
    public interface ICaseBookApiClient : IDI
    {
        Task<SearchCaseResponseDto> SearchCaseAsync(SearchCaseRequestDto request);
        Task<FileResponseDto> GetArchiveAsync(GetArchiveRequestDto request);
        Task<GetOrganizationMessagesResponseDto> GetGetOrganizationMessagesAsync(GetOrganizationMessagesRequestDto request);
        Task<GetOrganizationInfoResponseDto> GetOrganizationInfoAsync(GetOrganizationInfoRequestDto request);
        Task<SearchCaseResponseDto> GetCitizenBankruptcyAsync(GetCitizenBankruptcyRequestDto requestDto);
        Task<PledgesResponseDto> GetPledgesMessagesAsync(GetPledgesRequestDto request);
        Task<GetExecutoryMessagesResponseDto> GetExecutoryMessagesAsync(GetExecutoryMessagesRequestDto request);
    }
}