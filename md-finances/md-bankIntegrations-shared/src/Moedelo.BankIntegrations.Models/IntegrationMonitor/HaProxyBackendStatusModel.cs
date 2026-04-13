namespace Moedelo.BankIntegrations.Models.IntegrationMonitor
{
    public class HaProxyBackendStatusModel
    {
        public string HaProxyHost { get; set; }
        public string BackendName { get; set; }
        public string ServerName { get; set; }
        public string Status { get; set; }
        public int? ChecksFailed { get; set; }
        public int? DownTransitions { get; set; }
        public int? LastChangeSeconds { get; set; }
    }
}
