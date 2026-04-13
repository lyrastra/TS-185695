using Moedelo.Billing.Shared.Enums;

namespace Moedelo.Billing.Abstractions.Bills.Dto;

public class GetPackageParametersRequestDto
{
    public int FirmId { get; set; }
    public string ProductConfigurationCode { get; set; }
    public int? Duration { get; set; }
    public BillCreationSource CreationSource { get; set; }
}