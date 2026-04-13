using System;

namespace Moedelo.Billing.Abstractions.Dto.BizToAccTransfer.RevertTransfer.State;

public class SwitchOnBizPaymentStateDto
{
    public int BizPaymentId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsDownload { get; set; }
    public int AccPaymentId { get; set; }
    public bool Success { get; set; }
}