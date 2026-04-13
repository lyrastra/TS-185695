using Moedelo.BankIntegrations.ApiClient.Dto.BankOperation;
using Moedelo.BankIntegrations.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.InitIntegration
{
    public class IntegrationTurnRequestDto
    {
        public IntegrationIdentityDto IdentityDto { get; set; }

        /// <summary> включение / выключение интеграции </summary>
        public bool IsOn { get; set; }

        //TODO: нужно отвязаться и вычислять в сервисе интеграции
        public bool IsBank { get; set; }
        
        public IntegrationSource IntegrationSource { get; set; }
    }
}
