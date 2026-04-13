namespace Moedelo.RptV2.Dto.TradingTax
{
    public class TradingTaxPaymentDataDto
    {
        public string Number { get; set; }

        public string TaxCode { get; set; }

        public decimal Summ { get; set; }

        public string Reason { get; set; }

        public int Quarter { get; set; }

        public string Oktmo { get; set; }
    }
}
