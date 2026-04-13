using System;
using Moedelo.Billing.Shared.Enums;

namespace Moedelo.Billing.Abstractions.Legacy.Dto;

public class PaymentPositionDto
{
    public PaymentPositionType Type { get; set; }

    public OneTimeServiceType? OneTimeType { get; set; }

    public decimal Price { get; set; }

    public string Name { get; set; }

    public bool HasNds { get; set; }

    public decimal RegionalRatio { get; set; } = 1;

    public string ProductCode { get; set; }

    public string ProductConfigurationCode { get; set; }

    public DateTime? StartDate { get; set; } = null;

    public DateTime? EndDate { get; set; } = null;

    public decimal? NormativePrice { get; set; }
}