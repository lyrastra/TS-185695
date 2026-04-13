namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationDisableDetails;

public class IntegrationDisableDetailsResponseDto
{
    /// <summary>
    /// Идентификатор фирмы
    /// </summary>
    public int FirmId { get; set; }
    
    /// <summary>
    /// Идентификатор в таблице IntegrationRequest
    /// </summary>
    public int IntegrationRequestId { get; set; }
    
    /// <summary>
    /// Логин пользователя-инициатора ручного выключения 
    /// </summary>
    public string InitiatorLogin { get; set; }
    
    /// <summary>
    /// Причина отключения интеграции
    /// </summary>
    public string Message { get; set; }
}