#nullable enable
using System;
using Moedelo.Billing.Shared.Enums.AutoBilling;

namespace Moedelo.Billing.Abstractions.AutoBilling.Dto;

public sealed class GetAutoBillingReportRequestDto
{
    public int[]? InitiateIds { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public ProductTypeEnum ProductType { get; set; }
}