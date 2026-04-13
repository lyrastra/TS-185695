using System.Collections.Generic;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.PaymentEvents.GenerateEventFiles;

public class EventFilesGroupDto
{
    public string Category { get; set; }
    public IReadOnlyList<EventFileDataDto> Files { get; set; }
}