using Moedelo.Common.Enums.Enums.Evotor;
using System;
using System.Collections.Generic;

namespace Moedelo.CashV2.Dto.Evotor
{
    public class EvotorSessionInfoDto
    {
        public int Id { get; set; }

        public DateTime OpeningDate { get; set; }

        public string StoreName { get; set; }

        public string DeviceName { get; set; }

        public ZReportInfoDto ZReportInfo { get; set; }

        public RetailReportInfoDto RetailReportInfo { get; set; }

        public List<RetailRefundInfoDto> RetailRefundInfoList { get; set; } = new List<RetailRefundInfoDto>();

        public DateTime? ClosingDate { get; set; }
    }

    public class ZReportInfoDto
    {
        public ZReportStatus Status { get; set; }

        public decimal? Sum { get; set; }

        public long? DocumentBaseId { get; set; }
    }

    public class RetailReportInfoDto
    {
        public RetailReportStatus Status { get; set; }

        public long? DocumentBaseId { get; set; }
    }

    public class RetailRefundInfoDto
    {
        public RetailRefundStatus Status { get; set; }

        public decimal Sum { get; set; }

        public long? DocumentBaseId { get; set; }
    }
}