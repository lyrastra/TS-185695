using System;
using Moedelo.Billing.Shared.Enums.AutoBilling;

namespace Moedelo.Billing.Abstractions.AutoBilling.Dto;

public sealed class RequestDto
{
    public int Id { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime ModifiedDate { get; set; }
    public int FirmId { get; set; }
    public int InitiateId { get; set; }
    public RequestState State { get; set; }
}