using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeGeneration.InitialData.ChargeSettings;

public class WorkerContractSettingsDto
{
    public long Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int ChargeTypeId { get; set; }
    public decimal Sum { get; set; }
    public DateTime? ChargeDate { get; set; }
    public bool IsRegular { get; set; }
    public bool UseRussianDomains { get; set; }
}