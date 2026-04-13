using System;

namespace Moedelo.PayrollV2.Client.SalarySettings.DTO
{
    public class FirmFactorDto
    {
        public long Id { get; set; }

        public int FirmId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int? DivisionId { get; set; }

        public decimal RegionRate { get; set; }

        public int Type { get; set; }
    }
}
