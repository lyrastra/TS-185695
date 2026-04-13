namespace Moedelo.Billing.Abstractions.Dto.PurchasedServices;

public class PurchasedServicesDto
{
    public PurchasedServiceDto[] Services { get; set; }
    public PurchasedServiceDto[] AdditionalOptions { get; set; }
}