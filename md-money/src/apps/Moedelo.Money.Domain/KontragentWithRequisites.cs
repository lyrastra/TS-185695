namespace Moedelo.Money.Domain
{
    public class KontragentWithRequisites : IContractorWithRequisites
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? Form { get; set; }

        public string Inn { get; set; }

        public string Kpp { get; set; }

        public string SettlementAccount { get; set; }

        public string BankName { get; set; }
        
        public string BankBik { get; set; }

        public string BankCorrespondentAccount { get; set; }

        public bool IsCurrency { get; set; }

        public bool IsArchive { get; set; }
    }
}