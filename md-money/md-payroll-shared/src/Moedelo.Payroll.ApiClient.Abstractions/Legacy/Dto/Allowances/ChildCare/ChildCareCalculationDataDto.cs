namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.ChildCare
{
    public class ChildCareCalculationDataDto
    {
        public decimal MrotRate { get; set; }
        public InsuranceExperienceAllowanceDto InsuranceExperience { get; set; }
        public InsuranceExperienceAllowanceDto NotInsuranceExperience { get; set; }
    }
}