using System;

namespace Moedelo.RptV2.Dto.DownloadReport
{
    public class DownloadSzvExperiencePeriodRequestDto
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string ExperienceCode { get; set; }

        public string TerritoryCode { get; set; }

        public decimal? TerritoryRate { get; set; }
    }
}
