namespace Moedelo.PayrollV2.Dto.Fss
{
    public class MedicalCheckStepRequestDto
    {
        public int Period { get; set; }

        public int Year { get; set; }

        public MedicalCheckStepDto PrevData { get; set; }
    }
}
