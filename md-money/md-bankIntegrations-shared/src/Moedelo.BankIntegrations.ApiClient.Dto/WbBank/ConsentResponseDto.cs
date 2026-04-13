namespace Moedelo.BankIntegrations.ApiClient.Dto.WbBank;

/// <summary>
/// DTO для информации о согласиях клиента
/// </summary>
public class ConsentResponseDto
{
    public string Id { get; set; }
    public int IsConsentToPdn { get; set; }
    public int IsConsentToAds { get; set; }
}
