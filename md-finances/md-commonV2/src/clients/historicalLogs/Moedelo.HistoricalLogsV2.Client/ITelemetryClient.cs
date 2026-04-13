using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.HistoricalLogsV2.Client
{
    public interface ITelemetryClient : IDI
    {
        void SendEvent(int firmId, int userId, string eventName, string eventBody);
    }
}