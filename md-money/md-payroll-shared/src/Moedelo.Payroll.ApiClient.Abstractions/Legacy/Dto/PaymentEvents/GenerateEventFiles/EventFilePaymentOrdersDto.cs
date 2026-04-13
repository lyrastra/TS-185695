using System.Collections.Generic;
using Moedelo.Payroll.Enums.PaymentEvents;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.PaymentEvents.GenerateEventFiles;

public class EventFilePaymentOrdersDto
{
    public EventFileFormat[] Formats { get; set; }
    public IReadOnlyList<EventFilePaymentOrderDataDto> Items { get; set; }
    public bool IsSeparate { get; set; }
}