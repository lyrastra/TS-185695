namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.ChildBirth
{
    public class ChildBirthFullAllowanceDto
    {
        public ChildBirthFullAllowanceDto()
        {
            Allowance = new ChildBirthAllowanceDto();
            AllowanceEx = new ChildBirthAllowanceExDto();
        }
        
        public ChildBirthAllowanceDto Allowance { get; set; }
        public ChildBirthAllowanceExDto AllowanceEx { get; set; }
    }
}