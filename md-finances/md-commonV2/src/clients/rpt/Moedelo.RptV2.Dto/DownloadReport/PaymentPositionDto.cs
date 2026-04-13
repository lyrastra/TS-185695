using System;

namespace Moedelo.RptV2.Dto.DownloadReport
{
    public class PaymentPositionDto
    {
        public string Type { get; set; }

        public decimal Price { get; set; }

        public string Name { get; set; }

        public bool HasNds { get; set; }

        public string ProductCode { get; set; }

        public DateTime? StartDate { get; set; }
    }
}