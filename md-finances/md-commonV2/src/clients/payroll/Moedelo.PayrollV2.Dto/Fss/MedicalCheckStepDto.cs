namespace Moedelo.PayrollV2.Dto.Fss
{
    public class MedicalCheckStepDto
    {
        public Table5Dto Table5 { get; set; } = new Table5Dto();
        
        public bool? IsSpecialAssessmentDone { get; set; }
    }
}
