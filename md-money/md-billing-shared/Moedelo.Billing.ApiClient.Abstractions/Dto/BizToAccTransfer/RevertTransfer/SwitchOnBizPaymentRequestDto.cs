using System;

namespace Moedelo.Billing.Abstractions.Dto.BizToAccTransfer.RevertTransfer;

public class SwitchOnBizPaymentRequestDto
{
    public int BizPaymentId { get; set; }
    public int AccPaymentId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsDownload { get; set; }
}