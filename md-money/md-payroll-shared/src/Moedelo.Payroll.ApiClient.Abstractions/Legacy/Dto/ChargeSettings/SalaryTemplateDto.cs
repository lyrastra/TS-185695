namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeSettings
{
    public class SalaryTemplateDto
    {
        public int WorkerId { get; set; }
        
        public decimal Sum { get; set; }
        
        public SalaryPayType PayType { get; set; }
    }
}