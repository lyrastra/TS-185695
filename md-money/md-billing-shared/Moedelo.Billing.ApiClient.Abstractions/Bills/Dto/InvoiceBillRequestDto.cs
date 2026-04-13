using System.Collections.Generic;
using Moedelo.Billing.Shared.Enums;

namespace Moedelo.Billing.Abstractions.Bills.Dto;

public class InvoiceBillRequestDto
{
    public int FirmId { get; set; }
    public string Email { get; set; }
    public int? Duration { get; set; }
    public string ProductConfigurationCode { get; set; }
    public BillCreationSource CreationSource { get; set; }
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    public IReadOnlyCollection<OverridenModifierRequest>? OverridenModifiers { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
}