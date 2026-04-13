namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.SickListData
{
    public class SickListCalculationDataRequestDto
    {
        public int WorkerId { get; set; }
        
        public short YearFirst { get; set; }
        
        public short YearSecond { get; set; }
    }
}