using System;
using System.Collections.Generic;
using Moedelo.Payroll.Enums.PaymentEvents;
using Moedelo.Payroll.Enums.Worker;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.PaymentEvents.GenerateEventFiles;

public class EventFilePaybillDto
{
    public decimal Sum { get; set; }
    public WorkerPaymentType WorkerType { get; set; }
    public DateTime Date { get; set; }
    public string Number { get; set; }
    public string Destination { get; set; }
    public DateTime PaybillDate { get; set; }
    public string PaybillNumber { get; set; }
    public IReadOnlyList<EventFilePaybillWorkerDto> Workers { get; set; }
    public EventFileFormat[] Formats { get; set; }
}