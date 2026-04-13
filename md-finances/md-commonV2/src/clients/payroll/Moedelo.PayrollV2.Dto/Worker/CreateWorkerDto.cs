namespace Moedelo.PayrollV2.Dto.Worker
{
    public class CreateWorkerDto
    {
        public WorkerDto Worker { get; set; }
        
        public int FirmId { get; set; }

        public string ExecutionFunctionCode { get; set; }
    }
}