using System;
using Moedelo.Common.Enums.Enums;

namespace Moedelo.RptV2.Dto.DownloadReport
{
    public class DownloadPaymentOrderRequestDto
    {
        public int FirmId { get; set; }

        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        public string BillNumber { get; set; }

        public string UserLogin { get; set; }

        public int TariffId { get; set; }

        public DocumentFormat Format { get; set; }
    }
}
