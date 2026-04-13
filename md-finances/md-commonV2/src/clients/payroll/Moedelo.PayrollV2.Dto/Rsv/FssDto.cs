using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Payroll;

namespace Moedelo.PayrollV2.Dto.Rsv
{
    public class FssDto
    {
        public List<TemporaryDisabilityDto> TemporaryDisability { get; set; } = new List<TemporaryDisabilityDto>();
        public InsuranceCasesDto InsuranceCases { get; set; } = new InsuranceCasesDto();
        public FssPaymentType PaymentType { get; set; }
    }
}
