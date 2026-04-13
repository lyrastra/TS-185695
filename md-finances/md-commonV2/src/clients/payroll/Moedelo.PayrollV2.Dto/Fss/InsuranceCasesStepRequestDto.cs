namespace Moedelo.PayrollV2.Dto.Fss
{
    public class InsuranceCasesStepRequestDto
    {
        public int Period { get; set; }

        public int Year { get; set; }

        public InsuranceCasesStepDto PrevData { get; set; }
    }
}
