using System;

namespace Moedelo.OutSystemsIntegrationV2.Dto.Vacancies
{
    public class VacancyDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Source { get; set; }

        public string VacancyUrl { get; set; }

        public string VacancyDataSource { get; set; }

        public int SalaryMin { get; set; }

        public int SalaryMax { get; set; }

        public string Employment { get; set; }

        public string Schedule { get; set; }

        public string Duty { get; set; }

        public string Category { get; set; }

        public string Currency { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public VacancyAddressDto[] Addresses { get; set; }

        public VacancyRegionDto Region { get; set; }

        public VacancyCompanyDto Company { get; set; }

        public VacancyRequirementDto Requirement { get; set; }

        public VacancyTermDto Term { get; set; }
    }
}
