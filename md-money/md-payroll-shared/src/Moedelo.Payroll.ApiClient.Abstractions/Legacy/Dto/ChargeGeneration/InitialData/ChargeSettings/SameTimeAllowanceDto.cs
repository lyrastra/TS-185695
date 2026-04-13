using System;
using Moedelo.Payroll.Shared.Enums.Charge;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeGeneration.InitialData.ChargeSettings;

public class SameTimeAllowanceDto
{
    public long CustomChargeId { get; set; }
    public DateTime DeathDate { get; set; }
    public SameTimeAllowanceRecipient RecipientType { get; set; }
}