namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.EarlyPregnancy
{
    public class EarlyPregnancyFullAllowanceDto
    {
        public EarlyPregnancyFullAllowanceDto()
        {
            Allowance = new EarlyPregnancyAllowanceDto();
            AllowanceEx = new EarlyPregnancyAllowanceExDto();
        }
        
        public EarlyPregnancyAllowanceDto Allowance { get; set; }
        public EarlyPregnancyAllowanceExDto AllowanceEx { get; set; }
    }
}