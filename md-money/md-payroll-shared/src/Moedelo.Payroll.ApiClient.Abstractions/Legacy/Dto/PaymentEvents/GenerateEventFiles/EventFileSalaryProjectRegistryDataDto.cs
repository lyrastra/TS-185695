using System;
using System.Collections.Generic;
using Moedelo.Payroll.Enums.PaymentMethods;
using Moedelo.Payroll.Enums.Worker;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.PaymentEvents.GenerateEventFiles;

public class EventFileSalaryProjectRegistryDataDto
{
    public WorkerNdflStatus NdflStatus { get; set; }
    public IReadOnlyList<int> WorkerIds { get; set; }
    public string Number { get; set; }
    public DateTime Date { get; set; }
    public decimal Sum { get; set; }
    public int RegistryNumber { get; set; }
    public PaymentMethodType DocumentType { get; set; }
    public WorkerPaymentType WorkerType { get; set; }
    public IReadOnlyList<EventFileSalaryProjectPaymentChargeDto> ChargePayments { get; set; }
}