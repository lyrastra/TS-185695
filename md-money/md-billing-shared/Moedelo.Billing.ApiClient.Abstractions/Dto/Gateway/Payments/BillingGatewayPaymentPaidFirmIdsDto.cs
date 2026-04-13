using System;
using Moedelo.Billing.Shared.Enums;

namespace Moedelo.Billing.Abstractions.Dto.Gateway.Payments;

public class BillingGatewayPaymentPaidFirmIdsDto
{
    /// <summary>
    /// Дата выборки
    /// </summary>
    public DateTime SamplingDate { get; set; }
    
    /// <summary>
    /// Типы методов оплат. Опционально
    /// </summary>
    public PaymentMethodType[] PaymentMethodTypes { get; set; }

    /// <summary>
    /// Методы оплат. Опционально
    /// </summary>
    public string[] PaymentMethods { get; set; }
}