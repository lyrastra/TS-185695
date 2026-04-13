using System.Collections.Generic;
using Moedelo.Billing.Shared.Enums;

namespace Moedelo.Billing.Abstractions.Dto;

public class BillRequestDto
{
    public int FirmId { get; set; }

    public bool IsTechnicalBill { get; set; }

    public string PaymentMethod { get; set; }

    public string PromoCode { get; set; }

    public IReadOnlyCollection<ProductConfigurationRequestDto> ProductConfigurations { get; set; }

    public BillCreationSource CreationSource { get; set; }

    public bool IsCrossSelling { get; set; }

    /// <summary>
    /// Адрес почты клиента для отправки счёта
    /// </summary>
    public string ClientNotificationEmail { get; set; }

    /// <summary>
    /// Не отправлять счёт клиенту
    /// </summary>
    public bool NotSendEmailToClient { get; set; }
}