namespace Moedelo.PayrollV2.Dto.Rsv
{
    public class FssStepRequestDto
    {
        public int Period { get; set; }

        public int Year { get; set; }

        public FssDto PrevData { get; set; }
    }
}