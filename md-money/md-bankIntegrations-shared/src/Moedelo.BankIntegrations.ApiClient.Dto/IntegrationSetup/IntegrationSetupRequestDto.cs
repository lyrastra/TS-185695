using Moedelo.BankIntegrations.Enums;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationSetup;

public class IntegrationEnableSetupRequestDto
{
    /// <summary>
    /// Из какого раздела интеграция была подключена
    /// </summary>
    public IntegrationSource IntegrationSource { get; set; }
    
    /// <summary>
    /// Идентификатор фирмы
    /// </summary>
    public int FirmId { get; set; }

    /// <summary>
    /// Интеграционный партнер
    /// </summary>
    public IntegrationPartners IntegrationPartner { get; set; }

    /// <summary>
    /// Дополнительные данные необходимые для интеграции
    /// </summary>
    public string IntegrationData { get; set; }
    
    /// <summary>
    /// Внешний идентификатор клиента в системе интеграционного партнера
    /// </summary>
    public string ExternalClientId { get; set; }
    
    /// <summary>
    /// Информация о токене
    /// </summary>
    public string TokenData { get; set; }
}