using System;
using Moedelo.Payroll.Enums.Worker;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker
{
    public class TaxationStatusHistoryDto
    {
        public long Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int FirmId { get; set; }

        public int? WorkerId { get; set; }

        public int? DivisionId { get; set; }

        public TaxationSystemType TaxationSystem { get; set; }

        public virtual int? PatentId { get; set; }
    }
}