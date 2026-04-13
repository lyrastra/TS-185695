namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationMonitor
{
    public class HealthCheckResultDto
    {
        public KafkaHealthCheckResultDto Kafka { get; set; }
        public HaProxyHealthCheckResultDto HaProxy { get; set; }
    }
}
