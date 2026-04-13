namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Funds;

public class SfrPaymentDataDto
{
    public decimal Amount { get; set; }
    public bool HasUnpaidFundPayments { get; set; }
}