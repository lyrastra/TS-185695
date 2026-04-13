using Moedelo.Common.Enums.Enums.Kontragents;

namespace Moedelo.KontragentsV2.Dto
{
    public class KontragentWithSettlementAccountDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? BankId { get; set; }

        public string BankName { get; set; }

        public string Bik { get; set; }

        public string SettlementAccount { get; set; }
        
        public string Inn { get; set; }

        public KontragentForm? Form { get; set; }
        
        public bool IsFormalizedAddress { get; set; }

        public int? CommissionAgentId { get; set; }
    }
}