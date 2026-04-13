using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Efs.Injured
{
    public class EfsInjuredContractDto
    {
        public int WorkerId { get; set; }

        public long Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsInjuredCharge { get; set; }

        public bool IsTaxable { get; set; }
    }
}