namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.PaymentEvents.GenerateEventFiles;

public class EventFileSalaryProjectPaymentChargeDto
{
    public int WorkerId { get; set; }
    public int? ChargeTypeId { get; set; }
    public int PeriodYear { get; set; }
    public int PeriodMonth { get; set; }
    public decimal Sum { get; set; }
    public bool CanApplyDeduction { get; set; }
}