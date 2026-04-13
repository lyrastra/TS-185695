using System.Collections.Generic;
using Moedelo.Payroll.Enums.Worker;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker;

public class UpdateWorkingConditionsDto
{
    public IReadOnlyCollection<int> WorkerIds { get; set; }
    public WorkingConditionClass? WorkingConditionClass { get; set; }
    public string WorkplaceNumber { get; set; } 
}