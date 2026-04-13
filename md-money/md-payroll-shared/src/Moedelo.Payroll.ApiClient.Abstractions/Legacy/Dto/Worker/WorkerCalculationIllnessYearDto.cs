namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker
{
    public class WorkerCalculationIllnessYearDto
    {
        public int Year { get; set; }

        public decimal Sum { get; set; }

        public int ExcludedDaysCount { get; set; }
    }
}