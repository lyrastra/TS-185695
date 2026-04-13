using Moedelo.Payroll.Shared.Enums.Charge;
using Moedelo.Payroll.Shared.Enums.Common;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeGeneration.InitialData.ChargeSettings;

public class SpecialScheduleTypeDto
{
    public ChargeType ChargeType { get; set; }
    public DayInfoType ParentCode { get; set; }
}