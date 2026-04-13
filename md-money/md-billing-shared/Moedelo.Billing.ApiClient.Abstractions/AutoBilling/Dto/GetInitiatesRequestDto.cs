#nullable enable
using Moedelo.Billing.Shared.Enums.AutoBilling;

namespace Moedelo.Billing.Abstractions.AutoBilling.Dto;

public sealed class GetInitiatesRequestDto
{
    public int[]? Ids { get; set; }
    public InitiateType[]? Types { get; set; }
    public int Limit { get; set; } = 100;
    public int Offset { get; set; } = 0;
    public ProductTypeEnum? ProductType { get; set; }
}