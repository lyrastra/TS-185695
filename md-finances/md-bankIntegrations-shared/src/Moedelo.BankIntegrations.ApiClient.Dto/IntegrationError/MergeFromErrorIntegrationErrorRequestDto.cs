using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationError;

public class MergeFromErrorIntegrationErrorRequestDto
{
    /// <summary>
    /// Идентификатор фирмы
    /// </summary>
    public int FirmId { get; set; }
    
    /// <summary>
    /// Интеграционный партнер
    /// </summary>
    public IntegrationPartners Partner { get; set; }
    
    /// <summary>
    /// Текст ошибки
    /// </summary>
    public string Error { get; set; }
}