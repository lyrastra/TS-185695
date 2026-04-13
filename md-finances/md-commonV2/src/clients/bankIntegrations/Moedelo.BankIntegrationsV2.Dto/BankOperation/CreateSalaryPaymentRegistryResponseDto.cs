using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Integration;

namespace Moedelo.BankIntegrationsV2.Dto.BankOperation
{
    public class CreateSalaryPaymentRegistryResponseDto
    {
        public string Message { get; set; }

        public IntegrationResponseStatusCode Status { get; set; }

        public List<SalaryPaymentRegistryErrorDto> PaymentErrors { get; set; }
}
}
