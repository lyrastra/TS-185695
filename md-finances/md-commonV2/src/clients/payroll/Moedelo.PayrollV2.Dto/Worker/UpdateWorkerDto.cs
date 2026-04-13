namespace Moedelo.PayrollV2.Dto.Worker
{
    public class UpdateWorkerDto
    {
        public WorkerDto Worker { get; set; }
        
        public int FirmId { get; set; }

        public int WorkerId { get; set; }
    }
}