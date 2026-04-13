namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegratedUser
{
    public class IntegrationLimitsInfoDto
    {
        /// <summary>
        /// существует ограничение по количеству подключений или нет
        /// </summary>
        public bool IsHasLimit { get; set; }

        /// <summary>
        /// максимальное число доступных подключений
        /// </summary>
        public int MaxIntegration { get; set; }

        /// <summary>
        /// текущее количестиво активных подключений
        /// </summary>
        public int ActiveIntegration { get; set; }
    }
}
