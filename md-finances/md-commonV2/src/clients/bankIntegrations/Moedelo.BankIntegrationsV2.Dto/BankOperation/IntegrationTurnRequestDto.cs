using Moedelo.BankIntegrations.Enums;
using Moedelo.BankIntegrationsV2.Dto.Integrations;

namespace Moedelo.BankIntegrationsV2.Dto.BankOperation
{
    public class IntegrationTurnRequestDto
    {
        public IntegrationIdentityDto IdentityDto { get; set; }

        /// <summary> включение / выключение интеграции </summary>
        public bool IsOn { get; set; }

        //TODO: нужно отвязаться и вычислять в сервисе интеграции
        public bool IsBank { get; set; }
        
        /// <summary>
        /// Источник откуда включена интеграция
        /// </summary>
        public IntegrationSource IntegrationSource { get; set; }
    }
}
