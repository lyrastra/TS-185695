using System;
using Moedelo.Payroll.Enums.PaymentEvents;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.PaymentEvents;

public class PaymentEventInitialDataRequestDto
{
    public DateTime EventDate { get; set; }
    public PaymentEventType EventType { get; set; }
}