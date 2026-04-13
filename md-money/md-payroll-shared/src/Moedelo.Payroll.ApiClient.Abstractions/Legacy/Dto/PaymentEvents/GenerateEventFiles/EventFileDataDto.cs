using System.Collections.Generic;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.PaymentEvents.GenerateEventFiles;

public class EventFileDataDto
{
    public byte[] Data { get; set; }
    public string Name { get; set; }
    public string FullName { get; set; }
    public string Extension { get; set; }
    public IReadOnlyList<string> OriginalFileNames { get; set; }
}