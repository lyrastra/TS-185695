using System.Collections.Generic;

namespace Moedelo.BankIntegrations.ApiClient.Dto.DataInformation;

public class IntSummaryBySettlementsResponseDto
{
    public List<SettlementAccountStatusDto> Result { get; set; }
    public bool IsSuccess { get; set; }
    public string ErrorMsg { get; set; }
}