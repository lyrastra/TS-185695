namespace Moedelo.PayrollV2.Dto.Rsv
{
    public class LowTariffDto
    {
        public ItTariffDto ItTariff { get; set; } = new ItTariffDto();
        public UsnTariffDto UsnTariff { get; set; } = new UsnTariffDto();
        public PatentTariffDto PatentTariff { get; set; } = new PatentTariffDto();
    }
}