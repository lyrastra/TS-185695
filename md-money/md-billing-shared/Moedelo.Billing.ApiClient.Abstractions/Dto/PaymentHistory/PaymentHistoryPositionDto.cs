using System;
using System.Collections.Generic;
using Moedelo.Billing.Shared.Enums;

namespace Moedelo.Billing.Abstractions.Dto.PaymentHistory;

public class PaymentHistoryPositionDto
{
    public PaymentPositionType Type { get; set; }
    public OneTimeServiceType? OneTimeType { get; set; }
    public decimal Price { get; set; }
    public decimal MinPrice { get; set; }
    public string Name { get; set; }
    public bool HasNds { get; set; }
    public bool IsExcludedFrom1C { get; set; }
    public decimal RegionalRatio { get; set; } = 1.0m;
    public string ProductCode { get; set; } = null;
    public string ProductConfigurationCode { get; set; }
    public DateTime? StartDate { get; set; } = null;
    public DateTime? EndDate { get; set; } = null;
    public string NameForEmail { get; set; }
    public decimal? NormativePrice { get; set; }
    public List<PaymentHistoryPositionSellerDto> Sellers { get; set; }
}
