using Moedelo.Accounting.Enums.PaymentOrder;
using Moedelo.Accounting.Enums.PaymentOrder.BudgetaryPayment;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.PaymentEvents;

public class PaymentEventDeductionDto
{
    public string PaymentDescription { get; set; }
    public string Uin { get; set; }
    public string Kbk { get; set; }
    public string Oktmo { get; set; }
    public PaymentPriority PaymentPriority { get; set; }
    public BudgetaryPayerStatus PayerStatus { get; set; }
    public bool IsBudgetaryDebt { get; set; }
    public string DeductionWorkerDocumentNumber { get; set; }
    public long Id { get; set; }
    public int WorkerId { get; set; }
    public int KontragentId { get; set; }
    public decimal Sum { get; set; }
}