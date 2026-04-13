namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.Pregnancy
{
    public class PregnancyFullAllowanceDto
    {
        public PregnancyFullAllowanceDto()
        {
            Allowance = new PregnancyAllowanceDto();
            AllowanceEx = new PregnancyAllowanceExDto();
        }
        
        public PregnancyAllowanceDto Allowance { get; set; }
        public PregnancyAllowanceExDto AllowanceEx { get; set; }
    }
}