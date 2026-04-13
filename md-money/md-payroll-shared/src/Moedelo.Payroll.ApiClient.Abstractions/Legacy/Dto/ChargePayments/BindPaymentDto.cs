using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargePayments;

public class BindPaymentDto
{
    public int WorkerId { get; set; }
    public decimal Sum { get; set; }
    public long DocumentBaseId { get; set; }
    public DateTime PaymentDate { get; set; }
    public long ChargeId { get; set; }
    public bool IsPaid { get; set; }
}