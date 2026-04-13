using System;

namespace Moedelo.BillingV2.Dto.FrameInfo
{
    public class BillingFrameInfoPayHistoryDto
    {
        public int FirmId { get; set; }

        public string TariffName { get; set; }

        public decimal Sum { get; set; }

        public bool IsTrial { get; set; }

        public bool IsSuccess { get; set; }

        public string SalerLogin { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? PayDate { get; set; }

        public string Product { get; set; }

        public string CabinetType { get; set; }
    }
}
