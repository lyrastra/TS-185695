using System;

namespace Moedelo.Billing.Abstractions.AutoBilling.Dto.Report;

public sealed class AutoBillingReportRowDto
{
    public string OperatorLogin { get; set; }
    public string ClientLogin { get; set; }
    public string ProductCode { get; set; }
    public string CurrentBillNumber { get; set; }
    public string CurrentTariffName { get; set; }
    public DateTime? CurrentStartDate { get; set; }
    public DateTime? CurrentEndDate { get; set; }
    public string NewTariffName { get; set; }
    public string NewBillNumber { get; set; }
    public decimal? Cost { get; set; }
    public DateTime? NewStartDate { get; set; }
    public DateTime? NewEndDate { get; set; }
    public string RequestStatus { get; set; }
    public int? InitiateId { get; set; }
    public string InitiateStatus { get; set; }
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    public ExtendedOutDataDto? ExtendedOutData { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
}