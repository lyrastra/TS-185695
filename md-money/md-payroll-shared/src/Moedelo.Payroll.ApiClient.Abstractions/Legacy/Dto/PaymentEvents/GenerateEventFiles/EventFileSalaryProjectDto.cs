using System.Collections.Generic;
using Moedelo.Payroll.Enums.PaymentEvents;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.PaymentEvents.GenerateEventFiles;

public class EventFileSalaryProjectDto
{
    public EventFileFormat[] Formats { get; set; }
    public IReadOnlyList<EventFilePaymentOrderDataDto> PaymentOrders { get; set; }
    public IReadOnlyList<EventFileSalaryProjectRegistryDataDto> RegistriesData { get; set; }
    public bool IsSeparate { get; set; }
}