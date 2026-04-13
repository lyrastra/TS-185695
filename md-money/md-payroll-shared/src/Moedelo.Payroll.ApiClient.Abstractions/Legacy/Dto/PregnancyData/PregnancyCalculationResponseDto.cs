using System.Collections.Generic;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.PregnancyData
{
    public class PregnancyCalculationResponseDto
    {
        public PregnancyIncomeAndCalculationDto IncomeAndCalculation { get; set; }
        
        public Dictionary<string, List<string>> Errors { get; set; } = new Dictionary<string, List<string>>();
    }
}