using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeGeneration.InitialData.ChargeSettings;

public class BusinessTripDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public long? ExpensesSettingId { get; set; }
}