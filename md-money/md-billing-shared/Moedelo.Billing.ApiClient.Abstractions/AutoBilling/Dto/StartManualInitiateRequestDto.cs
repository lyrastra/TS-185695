using Moedelo.Billing.Shared.Enums.AutoBilling;

namespace Moedelo.Billing.Abstractions.AutoBilling.Dto;

public sealed class StartManualInitiateRequestDto
{
    public FirmValidationType FirmValidationType { get; set; }
    public BillValidationType BillValidationType { get; set; }
    public int OperatorUserId { get; set; }
    public int[] FirmIds { get; set; }
    public ProductTypeEnum ProductType { get; set; }
    public ExtraDataModeEnum ExtraDataMode { get; set; }
}