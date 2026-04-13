using System;
using System.Collections.Generic;

namespace Moedelo.PayrollV2.Client.ChargePayments.DTO
{
    public class WorkerChargePaymentsListDto
    {
        public long? DocumentBaseId { get; set; }

        public DateTime? DocumentDate { get; set; }

        public bool IsPaid { get; set; }

        public IList<WorkerChargePaymentsDto> WorkerChargePayments { get; set; }

        public WorkerChargePaymentsListDto()
        {
            WorkerChargePayments = new List<WorkerChargePaymentsDto>();
        }
    }
}
