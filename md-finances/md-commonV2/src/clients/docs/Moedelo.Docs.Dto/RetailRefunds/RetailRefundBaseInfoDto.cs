using System;

namespace Moedelo.Docs.Dto.RetailRefunds
{
    public class RetailRefundBaseInfoDto
    {
        public long DocumentBaseId { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }

        public int KontragentId { get; set; }

        public long StockId { get; set; }
    }
}