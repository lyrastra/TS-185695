namespace Moedelo.PayrollV2.Dto.Rsv
{
    public class TariffDto 
    {
        public string TariffCode { get; set; }
        public PfrDto Pfr { get; set; } = new PfrDto();
        public FfomsDto Ffoms { get; set; } = new FfomsDto();
        public decimal? PfrRate { get; set; }
    }
}
