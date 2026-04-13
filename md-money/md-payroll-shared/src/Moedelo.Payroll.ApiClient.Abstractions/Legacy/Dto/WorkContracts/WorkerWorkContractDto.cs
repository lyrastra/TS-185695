using System;
namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.WorkContracts;

public class WorkerWorkContractDto
{
    public long Id { get; set; }
        
    public int FirmId { get; set; }
        
    public DateTime PaymentDate { get; set; }
        
    public int WorkerId { get; set; }
        
    public string WorkerFio { get; set; }
}