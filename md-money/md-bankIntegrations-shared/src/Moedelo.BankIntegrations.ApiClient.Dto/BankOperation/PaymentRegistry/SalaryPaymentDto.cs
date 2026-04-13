using System;

namespace Moedelo.BankIntegrations.ApiClient.Dto.BankOperation.PaymentRegistry;

public class SalaryPaymentDto
{
    public int Number { get; set; }
    public string AccountNumber { get; set; }
    public string Purpose { get; set; }
    public string PayeeFirstName { get; set; }
    public string PayeeMiddleName { get; set; }
    public string PayeeLastName { get; set; }
    public decimal Sum { get; set; }
    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
}