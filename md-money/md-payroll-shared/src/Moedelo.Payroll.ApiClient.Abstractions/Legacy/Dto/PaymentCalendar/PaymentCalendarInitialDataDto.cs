using System;
using System.Collections.Generic;
using System.Linq;
using Moedelo.Payroll.Enums.SalarySettings;
using Moedelo.Payroll.Shared.Enums.Charge;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.PaymentCalendar;

public class PaymentCalendarInitialDataDto
{
    public IReadOnlyCollection<PaymentCalendarWorkerDto> Workers { get; set; } = 
        Array.Empty<PaymentCalendarWorkerDto>();

    public IReadOnlyCollection<PaymentCalendarChargeDto> Charges { get; set; } = 
        Array.Empty<PaymentCalendarChargeDto>();

    public PaymentCalendarSettingDto Setting { get; set; } = new();
    
    public IReadOnlyCollection<PaymentCalendarChargeDto> ChargesByPeriod(DateTime? startDate, DateTime? endDate)
    {
        var start = startDate ?? DateTime.MinValue;
        var end = endDate ?? DateTime.MaxValue;
        
        return Charges.Where(x => x.PaymentDeadline >= start && x.PaymentDeadline <= end).ToList();
    }

}

public class PaymentCalendarSettingDto
{
    public int DaySalaryPayment { get; set; }
    public SalaryPaymentPeriod SalaryPaymentPeriod { get; set; }
}

public class PaymentCalendarChargeDto
{
    public long Id { get; set; }
    public int WorkerId { get; set; }
    public DateTime ChargeDate { get; set; }
    public DateTime PaymentDeadline { get; set; }
    public PaymentCalendarChargeTypeDto Type { get; set; } = new();
    public bool IsPayKontragent { get; set; }
    public bool IsRegular { get; set; }
    public bool HasDeductionWithAutoPayment => DeductionIds?.Count > 0;
    public IReadOnlyCollection<long> DeductionIds { get; set; } = Array.Empty<long>();
    public decimal Sum { get; set; }
}

public class PaymentCalendarChargeTypeDto
{
    public ParentChargeTypeCode ParentCode { get; set; }
    public int Code { get; set; }
    public int NdflCode { get; set; }
    public string LongName { get; set; }
}

public class PaymentCalendarWorkerDto
{
    public int Id { get; set; }
    public string ShortName { get; set; }
}