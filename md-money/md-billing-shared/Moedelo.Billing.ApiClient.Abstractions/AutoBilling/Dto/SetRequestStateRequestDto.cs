using Moedelo.Billing.Shared.Enums.AutoBilling;

namespace Moedelo.Billing.Abstractions.AutoBilling.Dto;

public sealed class SetRequestStateRequestDto
{
    public int RequestId { get; set; }
    public RequestState RequestState { get; set; }
}