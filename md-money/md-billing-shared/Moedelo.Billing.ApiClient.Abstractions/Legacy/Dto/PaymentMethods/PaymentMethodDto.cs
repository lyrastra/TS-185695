using Moedelo.Billing.Shared.Enums;

namespace Moedelo.Billing.Abstractions.Legacy.Dto.PaymentMethods;

public class PaymentMethodDto
{
    public int Id { get; set; }
    public string Code { get; set; }
    public PaymentMethodType Type { get; set; }
    public bool IsActive { get; set; }
    public string Description { get; set; }
}