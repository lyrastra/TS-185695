using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.WorkerPositions
{
    public class PositionHistoryOrderUpdatingDto
    {
        public int PositionHistoryId { get; set; }

        public string OrderNumber { get; set; }

        public DateTime? OrderDate { get; set; }
    }
}
