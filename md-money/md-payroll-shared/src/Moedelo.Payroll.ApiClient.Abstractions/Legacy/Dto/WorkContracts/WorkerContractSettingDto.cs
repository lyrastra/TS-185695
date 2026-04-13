using System;
using Moedelo.Payroll.Shared.Enums.WorkerContractSetting;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.WorkContracts;

public class WorkerContractSettingDto
{
    public long Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public WorkerGpdType Type { get; set; }
    public string Name { get; set; }
}