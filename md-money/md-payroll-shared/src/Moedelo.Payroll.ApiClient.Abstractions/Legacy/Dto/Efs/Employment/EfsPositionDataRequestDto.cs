using System.Collections.Generic;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Efs.Employment;

public class EfsPositionDataRequestDto
{
    public int Year { get; set; }
    public int Month { get; set; }
    public IReadOnlyCollection<int> WorkerIds { get; set; }
}