namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto
{
    public class SettlementAccountExDto
    {
        public int SettlementAccountId { get; set; }
        
        public string SwiftCode { get; set; }
        
        public string EnglishBankName { get; set; }
        
        public string EnglishCompanyName { get; set; }
    }
}