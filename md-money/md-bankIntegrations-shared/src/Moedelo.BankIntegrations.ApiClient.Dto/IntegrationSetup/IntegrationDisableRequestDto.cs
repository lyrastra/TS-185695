using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationSetup;

public class IntegrationDisableRequestDto
{
    /// <summary>
    /// Идентификатор фирмы
    /// </summary>
    public int FirmId { get; set; }

    /// <summary>
    /// Интеграционный партнер
    /// </summary>
    public IntegrationPartners IntegrationPartner { get; set; }

    /// <summary>
    /// Ручное ли выключение
    /// </summary>
    public bool IsManual { get; set; }
    
    /// <summary>
    /// Идентификатор пользователя-инициатора ручного выключения 
    /// </summary>
    public int? InitiatorUserId { get; set; }
    
    /// <summary>
    /// Причина отключения интеграции
    /// </summary>
    public string Message { get; set; }
    
    /// <summary>
    /// Флаг сброса внешнего идентификатора
    /// </summary>
    public bool IsResetExternalClientId { get; set; }
}