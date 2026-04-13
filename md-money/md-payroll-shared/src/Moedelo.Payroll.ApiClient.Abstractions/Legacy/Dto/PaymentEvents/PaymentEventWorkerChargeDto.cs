using System;
using Moedelo.Payroll.Shared.Enums.Charge;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.PaymentEvents;

public class PaymentEventWorkerChargeDto
{
    public long Id { get; set; }
    public int WorkerId { get; set; }
    public DateTime Date { get; set; }
    public decimal Sum { get; set; }
    public decimal? DeductionSum { get; set; }
    public bool CanApplyDeduction { get; set; }
    public string Name { get; set; }
    public bool IsSelected { get; set; }
    public long? DocumentBaseId { get; set; }
    public bool IsDebt { get; set; }
    public ChargeType? ChargeType { get; set; }
    public int? ChargeTypeId { get; set; }
    public string LinkedPayment { get; set; }
    public string Description { get; set; }
}