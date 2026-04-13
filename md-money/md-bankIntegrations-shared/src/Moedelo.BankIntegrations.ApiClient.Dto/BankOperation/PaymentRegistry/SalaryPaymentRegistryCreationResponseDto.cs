using System.Collections.Generic;
using Moedelo.BankIntegrations.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.BankOperation.PaymentRegistry;

public class SalaryPaymentRegistryCreationResponseDto
{
    public string Message { get; set; }

    public IntegrationResponseStatusCode Status { get; set; }

    public List<SalaryPaymentRegistryErrorDto> PaymentErrors { get; set; }
}