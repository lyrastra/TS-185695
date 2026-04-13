using System;

namespace Moedelo.Billing.Abstractions.Dto.BizToAccTransfer.RevertTransfer.Context;

public class RevertTransferContextRequestDto
{
    public int AccFirmId { get; set; }
    public int BizFirmId { get; set; }
    public DateTime TransferDate { get; set; }
}