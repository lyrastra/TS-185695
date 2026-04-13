using System;

namespace Moedelo.BankIntegrationsV2.Dto.BankOperation
{
    public class SberbankStatementSummaryRequestDto
    {
        public int FirmId{ get; set; }
        public string AccountNumber { get; set; }
        public DateTime StatementDate { get; set; }
    }
}
