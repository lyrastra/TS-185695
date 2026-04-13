using System.Collections.Generic;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.PaymentEvents.GenerateEventFiles;

public class EventFilesRequestDto
{
    public EventFilePaymentOrdersDto PaymentOrders { get; set; }
    public EventFileCashOrdersDto CashOrders { get; set; }
    public EventFileSalaryProjectDto SalaryProject { get; set; }
    public EventFilePaybillDto Paybill { get; set; }
    public EventFilePaymentOrdersDto DeductionPayments { get; set; }
    public EventFilePaymentOrdersDto SfrInjuredPayment { get; set; }
    public IReadOnlyList<int> WorkerIds { get; set; }
    public string WorkerPaymentsCategory { get; set; }
    public string SalaryProjectPaymentsCategory { get; set; }
    public string DeductionPaymentsCategory { get; set; }
    public string SfrInjuredPaymentCategory { get; set; }
}