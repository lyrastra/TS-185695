using System;

namespace Moedelo.Billing.Abstractions.Bills.Dto;

public class TrialBillRequestDto
{
    public int FirmId { get; set; }
    public string ProductConfigurationCode { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}