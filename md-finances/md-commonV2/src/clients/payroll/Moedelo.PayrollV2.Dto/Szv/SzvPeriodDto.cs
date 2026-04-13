using System;

namespace Moedelo.PayrollV2.Dto.Szv
{
    public class SzvPeriodDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Name { get; set; }
        public string ExperienceCode { get; set; }
    }
}