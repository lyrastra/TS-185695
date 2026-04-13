using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeGeneration.InitialData.ChargeSettings;

public class SickListDto
{
    public long SpecialScheduleId { get; set; }
    public bool IsProlong { get; set; }
    public DateTime? ChargeDate { get; set; }
    public decimal PayOrg { get; set; }
    public int CountPaiedDays { get; set; }
}