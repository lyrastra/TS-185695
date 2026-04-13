using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.Pregnancy
{
    public class PregnancyAllowanceDto
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal RegionalRate { get; set; }

        public int Year1 { get; set; }

        public int Year2 { get; set; }

        public decimal Income1 { get; set; }

        public decimal Income2 { get; set; }

        public int PeriodDaysCount { get; set; }

        public string SickListNumber { get; set; }

        public string PrevSickListNumber { get; set; }

        public bool IsProlong { get; set; }

        /// <summary>Сумма, выплачиваемая ФСС</summary>
        public decimal PayFss { get; set; }

        /// <summary>Сумма, выплачиваемая организацией</summary>
        public decimal PayOrg { get; set; }

        /// <summary> Страховой стаж </summary>
        public InsuranceExperienceAllowanceDto InsuranceExperience { get; set; }

        public PregnancyAdditionalDataDto AdditionalData { get; set; }

        public DateTime? PrimaryStartDate { get; set; }
    }
}