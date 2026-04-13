using System;
using Moedelo.Payroll.Enums.Worker;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.PaymentEvents;

public class PaymentEventWorkerNdflStatusDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public WorkerNdflStatus Status { get; set; }
}