using System;
using Moedelo.Billing.Shared.Enums;

namespace Moedelo.Billing.Abstractions.Dto.Gateway.Payments;

/// <summary>
/// Описание позиции
/// </summary>
public class BillingGatewayPaymentPositionDto
{
    /// <summary>
    /// Код продукта
    /// </summary>
    public string ProductCode { get; set; }

    /// <summary>
    /// Наименование позиции
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Тип позиции
    /// </summary>
    public PaymentPositionType Type { get; set; }

    /// <summary>
    /// Дата начала срока действия позиции
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Дата окончания срока действия позиции
    /// </summary>
    public DateTime? EndDate { get; set; }
}

