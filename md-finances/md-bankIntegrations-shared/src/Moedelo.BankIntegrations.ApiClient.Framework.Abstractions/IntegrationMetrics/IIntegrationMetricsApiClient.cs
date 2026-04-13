using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationMetrics;

namespace Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.IntegrationMetrics;

public interface IIntegrationMetricsApiClient
{
    Task CreateTurnOnIntegrationMetricAsync(TurnOnIntegrationMetricDto dto);
}