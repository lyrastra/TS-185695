using System.Collections.Generic;

namespace Moedelo.BankIntegrationsV2.Dto.DataInformation
{
    public class IntSummaryBySettlementsResponseDto
    {
        public List<SettlementAccountStatusDto> Result { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMsg { get; set; }
    }
}