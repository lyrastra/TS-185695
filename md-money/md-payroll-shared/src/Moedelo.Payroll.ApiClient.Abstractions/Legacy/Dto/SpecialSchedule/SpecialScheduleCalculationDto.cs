using System;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.Pregnancy;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.SpecialSchedule
{
    public class SpecialScheduleCalculationDto
    {
        public int WorkeId { get; set; }
        public string WorkerSnils { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PaidByEmployer { get; set; }
        public decimal PaidByFss { get; set; }
        public string SickListNumber { get; set; }
        public DateTime StartOfWorkingDate { get; set; }
        public PlaceOfWorkType PlaceOfWorkType { get; set; }
        public DateTime? Form1Date { get; set; }
        public decimal YearFirstSum { get; set; }
        public decimal YearSecondSum { get; set; }
        
        public InsuranceExperienceAllowanceDto InsuranceExperience { get; set; } =
            new InsuranceExperienceAllowanceDto();

        public InsuranceExperienceAllowanceDto NotInsuranceExperience { get; set; } =
            new InsuranceExperienceAllowanceDto();

        public decimal BaseDailyAvgSalary { get; set; }

        public decimal RegionalRate { get; set; }
        public decimal MrotRate { get; set; }
        public int CountCalculateDays { get; set; }
    }
}