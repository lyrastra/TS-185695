using System;

namespace Moedelo.SberbankCryptoEndpointV2.Dto.old.Upg2.StatementSummary
{
    public class GetStatementSummaryRequestDto
    {
        public int FirmId { get; set; }
        public string AccountNumber { get; set; }
        public DateTime StatementDate { get; set; }
    }
}
