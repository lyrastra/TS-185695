using Moedelo.Billing.Shared.Enums;

namespace Moedelo.Billing.Abstractions.Legacy.Dto.PaymentMethods;

public class PaymentMethodSearchCriteriaDto
{
    public int[] Ids { get; set; }
    public string[] Codes { get; set; }
    public bool? IsActive { get; set; }
    public PaymentMethodType[] Types { get; set; }
}