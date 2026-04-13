using System;

namespace Moedelo.BankIntegrationsV2.Dto.DataInformation
{
    public class SettlementAccountV2Dto
    {
        public string BankFullName { get; set; }
        public string Bik { get; set; }
        public string SettlementNumber { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Currency { get; set; }
        public string TransitNumber { get; set; }
        public bool Blocked { get; set; }
        public bool Closed { get; set; }
    }
}