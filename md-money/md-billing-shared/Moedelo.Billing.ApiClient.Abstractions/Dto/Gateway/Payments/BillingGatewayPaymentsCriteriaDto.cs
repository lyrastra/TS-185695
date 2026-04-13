using Moedelo.Billing.Shared.Enums;

namespace Moedelo.Billing.Abstractions.Dto.Gateway.Payments;

// <summary>
/// Критерии для получения истории платежей фирмы
/// </summary>
public class BillingGatewayPaymentsCriteriaDto
{
    /// <summary>
    /// Идентификатор фирмы
    /// </summary>
    public int FirmId { get; set; }

    /// <summary>
    /// Типы методов оплат. Опционально
    /// </summary>
    public PaymentMethodType[] PaymentMethodTypes { get; set; }

    /// <summary>
    /// Методы оплат. Опционально
    /// </summary>
    public string[] PaymentMethods { get; set; }
}
