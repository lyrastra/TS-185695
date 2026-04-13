using System.Collections.Generic;
using Moedelo.Payroll.Enums.PaymentEvents;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.PaymentEvents.GenerateEventFiles;

public class EventFileCashOrdersDto
{
    public EventFileFormat[] Formats { get; set; }
    public IReadOnlyList<EventFileCashOrderDataDto> Items { get; set; }
}