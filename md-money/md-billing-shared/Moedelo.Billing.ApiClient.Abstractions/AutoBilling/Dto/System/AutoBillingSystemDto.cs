using System;
using Moedelo.Billing.Shared.Enums.AutoBilling;

namespace Moedelo.Billing.Abstractions.AutoBilling.Dto.System;

public sealed class AutoBillingSystemDto
{
    public int Id { get; set; }
    public DateTime CreateDate { get; set; }
    public int OperatorUserId { get; set; }
    public SystemStatus Status { get; set; }
    public FirmValidationType FirmValidationType { get; set; }
    public BillValidationType BillValidationType { get; set; }
    public ProductTypeEnum ProductType { get; set; }
    public ExtraDataModeEnum ExtraDataMode { get; set; }
}