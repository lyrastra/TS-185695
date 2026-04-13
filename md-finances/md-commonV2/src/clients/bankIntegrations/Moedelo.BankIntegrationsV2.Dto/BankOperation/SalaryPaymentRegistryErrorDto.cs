using System;
using System.Collections.Generic;

namespace Moedelo.BankIntegrationsV2.Dto.BankOperation
{
    public class SalaryPaymentRegistryErrorDto
    {
        public int Number { get; set; }
        public string AccountNumber { get; set; }
        public List<ErrorDto> Errors { get; set; }
    }
}