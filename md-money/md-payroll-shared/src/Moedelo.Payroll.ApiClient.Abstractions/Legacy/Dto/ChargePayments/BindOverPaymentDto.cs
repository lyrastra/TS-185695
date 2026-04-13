using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargePayments;

public class BindOverPaymentDto
{
    public long DocumentBaseId { get; set; }
    public long OverPaymentChargeId { get; set; }
    public long ChargeId { get; set; }
    public DateTime Date { get; set; }
    public decimal Sum { get; set; }
    public int WorkerId { get; set; }
}