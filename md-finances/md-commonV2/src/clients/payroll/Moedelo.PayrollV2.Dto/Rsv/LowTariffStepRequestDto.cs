namespace Moedelo.PayrollV2.Dto.Rsv
{
    public class LowTariffStepRequestDto
    {
        public int Period { get; set; }

        public int Year { get; set; }

        public LowTariffDto PrevData { get; set; }
    }
}