namespace Moedelo.PayrollV2.Dto.Szv
{
    public class SzvValidationResultDto
    {   
        public string WorkerName { get; set; }
        public int WorkerId { get; set; }
        public SzvPeriodDto Period1 { get; set; }
        public SzvPeriodDto Period2 { get; set; }
    }
}