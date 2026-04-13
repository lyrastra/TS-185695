using System;

namespace Moedelo.PaymentImport.Dto
{
    public class IdentifyOperationIgnoreNumberResponseDto
    {
        public Guid Guid { get; set; }

        public int? ImportRuleId { get; set; }

        public bool IsIgnoreNumber { get; set; }
    }
}
