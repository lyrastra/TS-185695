using System;

namespace Moedelo.Billing.Abstractions.Dto.BizToAccTransfer.RevertTransfer.Context;

public class RevertTransferPaymentContextDto
{
    public int Id { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public bool? IsDownload { get; set; }
}