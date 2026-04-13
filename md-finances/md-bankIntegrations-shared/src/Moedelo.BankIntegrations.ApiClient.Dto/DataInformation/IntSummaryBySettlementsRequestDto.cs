using System.Collections.Generic;

namespace Moedelo.BankIntegrations.ApiClient.Dto.DataInformation;

public class IntSummaryBySettlementsRequestDto
{
    public int FirmId { get; set; }
    public int UserId { get; set; }
    public List<SettlementAccountDto> Settlements { get; set; }
}