namespace Moedelo.Billing.Abstractions.Dto.BizToAccTransfer.SwitchOn;

public class SwitchOnTransferredPaymentRequestDto
{
    public int ToFirmId { get; set; }

    public int FromPaymentId { get; set; }

    public int ToPaymentId { get; set; }

    public int InvoicedPrimaryBillId { get; set; }
}