using System;
using System.Collections.Generic;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargePayments
{
    public class WorkerChargePaymentsListDto
    {
        public long? DocumentBaseId { get; set; }

        public DateTime? DocumentDate { get; set; }

        public bool IsPaid { get; set; }

        public IReadOnlyCollection<WorkerChargePaymentsDto> WorkerChargePayments { get; set; } = Array.Empty<WorkerChargePaymentsDto>();
    }
}
