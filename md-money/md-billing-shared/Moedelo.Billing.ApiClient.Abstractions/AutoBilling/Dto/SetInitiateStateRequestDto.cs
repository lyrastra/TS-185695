using Moedelo.Billing.Shared.Enums.AutoBilling;

namespace Moedelo.Billing.Abstractions.AutoBilling.Dto;

public sealed class SetInitiateStateRequestDto
{
    public int InitiateId { get; set; }
    public InitiateState InitiateState { get; set; }
}