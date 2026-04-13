using System.Collections.Generic;

namespace Moedelo.BankIntegrations.ApiClient.Dto.WbBank;

public class ClientInfoResponseDto
{
    public string Id { get; set; }
    public string ShortName { get; set; }
    public string Name { get; set; }
    public string Inn { get; set; }
    public string Kpp { get; set; }
    public string TaxSystem { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Fio { get; set; }
    public int IsConsentToPdn { get; set; }
    public int IsConsentToAds { get; set; }
    public List<AccountResponseDto> Accounts { get; set; }
}
