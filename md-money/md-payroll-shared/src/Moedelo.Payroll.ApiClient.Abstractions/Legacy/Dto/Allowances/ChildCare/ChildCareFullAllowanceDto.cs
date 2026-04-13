namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.ChildCare
{
    public class ChildCareFullAllowanceDto
    {
        public ChildCareFullAllowanceDto()
        {
            Allowance = new ChildCareAllowanceDto();
            AllowanceEx = new ChildCareAllowanceExDto();
        }
        
        public ChildCareAllowanceDto Allowance { get; set; }
        public ChildCareAllowanceExDto AllowanceEx { get; set; }
    }
}