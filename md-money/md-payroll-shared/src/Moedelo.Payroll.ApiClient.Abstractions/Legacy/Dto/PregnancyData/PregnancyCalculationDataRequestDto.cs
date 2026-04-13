namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.PregnancyData
{
    public class PregnancyCalculationDataRequestDto
    {
        public int WorkerId { get; set; }
        
        public short YearFirst { get; set; }
        
        public short YearSecond { get; set; }
    }
}