using System;

namespace Moedelo.SberbankCryptoEndpointV2.Dto.old.Upg2.Response
{
    public class SettlementAccountDto
    {
        public string BankFullName { get; set; }
        public string Bik { get; set; }
        public string SettlementNumber { get; set; }
        public string Currency { get; set; }
        public string TransitNumber { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public bool Blocked { get; set; }
        public bool Closed { get; set; }
    }
}
