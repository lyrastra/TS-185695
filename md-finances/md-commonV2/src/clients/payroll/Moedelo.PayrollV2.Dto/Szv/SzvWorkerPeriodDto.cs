using System;

namespace Moedelo.PayrollV2.Dto.Szv
{
    public class SzvWorkerPeriodDto
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string ExperienceCode { get; set; }

        public string TerritoryCode { get; set; }

        public decimal? TerritoryRate { get; set; }
    }
}