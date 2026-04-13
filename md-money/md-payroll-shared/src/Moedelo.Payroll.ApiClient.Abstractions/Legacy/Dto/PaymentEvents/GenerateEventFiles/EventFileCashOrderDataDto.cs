using System;
using Moedelo.Payroll.Enums.Worker;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.PaymentEvents.GenerateEventFiles;

public class EventFileCashOrderDataDto
{
    public decimal Sum { get; set; }
    public WorkerPaymentType WorkerType { get; set; }
    public DateTime Date { get; set; }
    public int WorkerId { get; set; }
    public string Number { get; set; }
    public string Destination { get; set; }
}