using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationError;

public class MergeFromGetStatementErrorRequestDto
{
    /// <summary>
    /// Идентификатор фирмы
    /// </summary>
    public int FirmId { get; set; }
    
    /// <summary>
    /// Последний идентификатор в таблице integrationRequest
    /// </summary>
    public int LastRequestId { get; set; }
    
    /// <summary>
    /// Номер счета
    /// </summary>
    public string SettlementAccount { get; set; }
    
    /// <summary>
    /// Интеграционный партнер
    /// </summary>
    public IntegrationPartners Partner { get; set; }
    
    /// <summary>
    /// Текст ошибки
    /// </summary>
    public string Error { get; set; }
}