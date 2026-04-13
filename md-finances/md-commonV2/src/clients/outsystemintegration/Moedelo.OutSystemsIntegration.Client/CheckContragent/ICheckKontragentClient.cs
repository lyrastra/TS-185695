using System.Collections.Generic;
using System.Threading.Tasks;

using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.OutSystemsIntegrationV2.Dto;

namespace Moedelo.OutSystemsIntegrationV2.Client.CheckContragent
{
    public interface ICheckKontragentClient : IDI
    {
        Task<List<CheckKontragentResponseDto>> CheckAsync(IList<CheckKontragentRequestDto> requestDto);

        Task<CheckKontragentResponseDto> CheckAsync(CheckKontragentRequestDto requestDto);

        Task UpdateKontragentStatusAsync(List<CheckKontragentRequestDto> requestDtoList);
    }
}