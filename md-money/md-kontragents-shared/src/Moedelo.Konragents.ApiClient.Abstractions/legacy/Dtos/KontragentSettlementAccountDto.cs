namespace Moedelo.Konragents.ApiClient.Abstractions.legacy.Dtos
{
    public class KontragentSettlementAccountDto
    {
        public long Id { get; set; }
        
        public int KontragentId { get; set; }
        
        public string Number { get; set; }
        
        public int? BankId { get; set; }

        public string BankName { get; set; }
        
        public string NonResidentBankName { get; set; }
        
        public bool IsActive { get; set; }
    }
}