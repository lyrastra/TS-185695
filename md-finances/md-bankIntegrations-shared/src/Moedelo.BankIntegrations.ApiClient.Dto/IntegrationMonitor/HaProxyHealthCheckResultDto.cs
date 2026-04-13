using System;
using System.Collections.Generic;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationMonitor
{
    public class HaProxyHealthCheckResultDto
    {
        public List<HaProxyBackendStatusDto> DownServers { get; set; } = new();
        public bool FetchError { get; set; }
        public string FetchErrorMessage { get; set; }
        public DateTime CheckedAtUtc { get; set; }
    }
}
