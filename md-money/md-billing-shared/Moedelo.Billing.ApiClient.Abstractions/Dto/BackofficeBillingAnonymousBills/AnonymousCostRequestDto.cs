using System.Collections.Generic;

namespace Moedelo.Billing.Abstractions.Dto.BackofficeBillingAnonymousBills;

public class AnonymousCostRequestDto
{
    public IReadOnlyCollection<ProductConfigurationRequestDto> ProductConfigurations { get; set; }
    public IReadOnlyDictionary<string, string> FirmProperties { get; set; }
    public bool IsCrossSelling { get; set; }
    public string PromoCode { get; set; }
}