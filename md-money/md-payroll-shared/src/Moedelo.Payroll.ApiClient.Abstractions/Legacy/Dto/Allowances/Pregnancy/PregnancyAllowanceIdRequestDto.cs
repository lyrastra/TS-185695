using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.Pregnancy
{
    public class PregnancyAllowanceIdRequestDto
    {
        public WorkerRequestDto Worker { get; set; }

        public string SickListNumber { get; set; }
    }
}