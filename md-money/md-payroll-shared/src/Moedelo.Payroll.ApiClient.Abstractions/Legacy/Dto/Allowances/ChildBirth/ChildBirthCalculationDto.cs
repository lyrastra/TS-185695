using System.Collections.Generic;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Validation;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.ChildBirth
{
    public class ChildBirthCalculationDto
    {
        public ChildBirthIncomeAndCalculationDto IncomeAndCalculation { get; set; }
        public IReadOnlyCollection<ValidationRuleResultDto> Errors { get; set; } = new List<ValidationRuleResultDto>();
    }
}