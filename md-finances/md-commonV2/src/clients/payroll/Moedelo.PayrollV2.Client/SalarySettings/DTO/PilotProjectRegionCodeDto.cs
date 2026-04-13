using System;

namespace Moedelo.PayrollV2.Client.SalarySettings.DTO
{
    public class PilotProjectRegionCodeDto
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public bool IsRequire { get; set; }

        public DateTime StartDate { get; set; }
    }
}
