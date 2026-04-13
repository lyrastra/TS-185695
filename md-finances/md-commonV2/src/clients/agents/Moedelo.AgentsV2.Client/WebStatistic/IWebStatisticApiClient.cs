using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AgentsV2.Dto.WebStatistics;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.AgentsV2.Client.WebStatistic
{
    public interface IWebStatisticApiClient : IDI
    {
        Task<WebStatisticsResponseDto> GetStatisticByPartnerAsync(WebStatisticsRequestDto request);
    }
}