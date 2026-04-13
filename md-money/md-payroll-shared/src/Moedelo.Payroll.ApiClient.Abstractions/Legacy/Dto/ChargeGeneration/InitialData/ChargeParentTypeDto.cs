using Moedelo.Payroll.Shared.Enums.Charge;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeGeneration.InitialData;

public class ChargeParentTypeDto
{
    public int Id { get; set; }
    public ParentChargeTypeCode Code { get; set; }
}