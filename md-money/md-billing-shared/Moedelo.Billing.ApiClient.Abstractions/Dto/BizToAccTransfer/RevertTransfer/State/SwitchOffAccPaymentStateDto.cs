using System;

namespace Moedelo.Billing.Abstractions.Dto.BizToAccTransfer.RevertTransfer.State;

public class SwitchOffAccPaymentStateDto
{
    public int AccPaymentId { get; set; }
    public bool IsTrial { get; set; }
    public bool IsDownload { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}