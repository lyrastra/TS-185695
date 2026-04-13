using Moedelo.Billing.Shared.Enums;
using System;

namespace Moedelo.Billing.Abstractions.Dto.YaPay;

public class YaPayOrderDto
{
    public int FirmId { get; set; }
    public Guid OrderGuid { get; set; }
    public int? PaymentHistoryId { get; set; }
    public bool IsGlavuchet { get; set; }
    public YaPayOrderStatus Status { get; set; }
}
