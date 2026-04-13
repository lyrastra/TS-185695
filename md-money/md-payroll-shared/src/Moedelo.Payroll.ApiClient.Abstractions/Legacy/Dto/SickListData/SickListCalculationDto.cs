using System.Collections.Generic;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Validation;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.SickListData
{
    public class SickListCalculationDto
    {
        public SickListIncomeAndCalculationDto IncomeAndCalculation { get; set; }
        
        public IReadOnlyCollection<ValidationRuleResultDto> Errors { get; set; } = new List<ValidationRuleResultDto>(); 
    }
}