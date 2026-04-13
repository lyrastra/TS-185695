namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.SickList
{
    public class SickListFullAllowanceDto
    {
        public SickListFullAllowanceDto()
        {
            Allowance = new SickListAllowanceDto();
            AllowanceEx = new SickListAllowanceExDto();
        }

        public SickListAllowanceDto Allowance { get; set; }
        public SickListAllowanceExDto AllowanceEx { get; set; }
    }
}