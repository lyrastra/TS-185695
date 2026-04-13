namespace Moedelo.PayrollV2.Dto.Fss
{
    public class ChargedAndPaidFeesStepRequestDto
    {
        public int Period { get; set; }

        public int Year { get; set; }

        public ChargedAndPaidFeesStepDto PrevData { get; set; }
    }
}
