namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationMonitor
{
    public class HaProxyBackendStatusDto
    {
        /// <summary>
        /// Хост HAProxy, с которого получен статус backend-сервера.
        /// </summary>
        public string HaProxyHost { get; set; }

        /// <summary>
        /// Название backend в конфигурации HAProxy.
        /// </summary>
        public string BackendName { get; set; }

        /// <summary>
        /// Название конкретного сервера внутри backend.
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// Текущее состояние сервера (например, UP, DOWN).
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Количество неуспешных health-check подряд.
        /// </summary>
        public int? ChecksFailed { get; set; }

        /// <summary>
        /// Количество переходов сервера в состояние DOWN.
        /// </summary>
        public int? DownTransitions { get; set; }

        /// <summary>
        /// Количество секунд с момента последнего изменения статуса.
        /// </summary>
        public int? LastChangeSeconds { get; set; }
    }
}
