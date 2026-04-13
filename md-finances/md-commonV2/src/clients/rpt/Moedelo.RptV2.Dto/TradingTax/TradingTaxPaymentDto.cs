using System.Collections.Generic;

namespace Moedelo.RptV2.Dto.TradingTax
{
    public class TradingTaxPaymentDto
    {
        public int DocNumber { get; set; }

        public string DocDate { get; set; }

        public int Year { get; set; }

        public string EndDate { get; set; }

        public List<TradingTaxPaymentDataDto> Objects { get; set; }
    }
}
