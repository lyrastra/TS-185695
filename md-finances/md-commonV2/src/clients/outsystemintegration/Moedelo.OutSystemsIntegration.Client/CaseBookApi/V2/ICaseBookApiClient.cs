using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.OutSystemsIntegrationV2.Dto;
using Moedelo.OutSystemsIntegrationV2.Dto.CaseBookApi;

namespace Moedelo.OutSystemsIntegrationV2.Client.CaseBookApi.V2
{
    public interface ICaseBookApiClient : IDI
    {
        Task<SearchCaseResponseDto> SearchCaseAsync(SearchCaseRequestDto request);
        Task<FileResponseDto> GetArchiveAsync(GetArchiveRequestDto request);
        Task<GetOrganizationMessagesResponseDto> GetGetOrganizationMessagesAsync(GetOrganizationMessagesRequestDto request);
        Task<GetOrganizationInfoResponseDto> GetOrganizationInfoAsync(GetOrganizationInfoRequestDto request);
    }
}