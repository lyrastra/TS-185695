using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.SickList
{
    public class SickListAllowanceIdRequestDto
    {
        public WorkerRequestDto Worker { get; set; }

        public string SickListNumber { get; set; }
    }
}