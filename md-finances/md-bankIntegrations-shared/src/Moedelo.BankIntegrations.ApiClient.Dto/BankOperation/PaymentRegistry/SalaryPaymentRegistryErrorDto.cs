using System.Collections.Generic;
using Moedelo.BankIntegrations.Dto.PaymentRegistries;

namespace Moedelo.BankIntegrations.ApiClient.Dto.BankOperation.PaymentRegistry;

public class SalaryPaymentRegistryErrorDto
{
    public int Number { get; set; }
    public string AccountNumber { get; set; }
    public List<ErrorDto> Errors { get; set; }
}