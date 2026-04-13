using System;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationMonitor
{
    public class KafkaConsumerAlertDto
    {
        public string AlertType { get; set; }
        public string GroupId { get; set; }
        public string Topic { get; set; }
        public string ServiceId { get; set; }
        public string State { get; set; }
        public int PausedPartitionsCount { get; set; }
        public double? MaxLagTimeSeconds { get; set; }
        public DateTime? OldestUnprocessedMessageTime { get; set; }
        public string AlertKey { get; set; }
        public string Description { get; set; }
        public DateTime DetectedAtUtc { get; set; }
    }
}
