#nullable enable
using System;
using Moedelo.Billing.Shared.Enums.AutoBilling;

namespace Moedelo.Billing.Abstractions.AutoBilling.Dto;

public sealed class GetRequestsRequestDto
{
    public int[] Ids { get; set; } = Array.Empty<int>();
    public int[] InitiateIds { get; set; } = Array.Empty<int>();
    public int[] FirmIds { get; set; } = Array.Empty<int>();
    public RequestState[] States { get; set; } = Array.Empty<RequestState>();
    public ProductTypeEnum ProductType { get; set; }
    public string? Login { get; set; }
    public string? Product { get; set; }
    public string? Tariff { get; set; }
    public decimal? CostFrom { get; set; }
    public decimal? CostTo { get; set; }
    public string? PaymentMethod { get; set; }
    public DateTime? StartDateFrom { get; set; }
    public DateTime? StartDateTo { get; set; }
    public DateTime? EndDateFrom { get; set; }
    public DateTime? EndDateTo { get; set; }
    public bool? AutoRenewal { get; set; }
    public string? SellerLogin { get; set; }
    public string? OperatorLogin { get; set; }

    public RequestsOrderBy? OrderBy { get; set; }
    public OrderDirection? OrderDirection { get; set; }

    public int Limit { get; set; } = 100;
    public int Offset { get; set; } = 0;
}