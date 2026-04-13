using System;
using Moedelo.Common.Enums.Enums.Billing;

namespace Moedelo.BillingV2.Dto.BillingInfo
{
    public class PaymentsWithExForFirmResponseDto
    {
        public int FirmId { get; set; }

        public BillingOperationType OperationType { get; set; }

        public string BillNumber { get; set; }

        public DateTime BillDate { get; set; }

        public int PriceListId { get; set; }

        public decimal Sum { get; set; }

        public bool IsSuccess { get; set; }

        public DateTime Date { get; set; }

        public DateTime? IncomingDate { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}