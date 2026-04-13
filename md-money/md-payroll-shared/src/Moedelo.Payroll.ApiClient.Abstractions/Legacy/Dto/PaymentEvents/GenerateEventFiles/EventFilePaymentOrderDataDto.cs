using System;
using Moedelo.Payroll.Enums.Worker;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.PaymentEvents.GenerateEventFiles;

public class EventFilePaymentOrderDataDto
{
    public int WorkerId { get; set; }
    public decimal Sum { get; set; }
    public DateTime OrderDate { get; set; }
    public string PaymentOrder { get; set; }
    public int PaymentNumber { get; set; }
    public int PurposeCode { get; set; }
    public WorkerPaymentType WorkerType { get; set; }
    public string RecipientName { get; set; }
}