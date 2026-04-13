namespace Moedelo.OutSystemsIntegrationV2.Dto.CaseBookApi.Arbitration
{
    /// <summary>
    /// Банкротство 
    /// </summary>
    public class CaseBankruptcyDto
    {
        public string CaseStage { get; set; }

        public string DebtorInn { get; set; }

        public string DebtorOgrn { get; set; }
        
        public bool IsDebtor { get; set; }
    }
}