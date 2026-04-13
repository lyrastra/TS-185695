using System.Collections.Generic;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.SickListData
{
    public class SickListCalculationResponseDto
    {
        public SickListIncomeAndCalculationDto IncomeAndCalculation { get; set; }
        
        public Dictionary<string, List<string>> Errors { get; set; } = new Dictionary<string, List<string>>(); 
    }
}