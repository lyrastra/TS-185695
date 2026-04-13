using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationMetrics;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.IntegrationMetrics
{
    public interface IIntegrationMetricsApiClient
    {
        Task CreateTurnOnIntegrationMetricAsync(TurnOnIntegrationMetricDto dto);
    }
}