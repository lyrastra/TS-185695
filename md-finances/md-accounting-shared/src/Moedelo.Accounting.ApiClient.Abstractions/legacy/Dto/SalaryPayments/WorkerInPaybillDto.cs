using Moedelo.Accounting.Enums;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.SalaryPayments
{
    public class WorkerInPaybillDto
    {
        public WorkerInPaybillDto()
        {
        }

        public WorkerInPaybillDto(int id, string name, decimal sum, TaxationSystemType tax)
        {
            Id = id;
            Sum = sum;
            Name = name;
            TaxationSystem = tax;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Sum { get; set; }

        public TaxationSystemType TaxationSystem { get; set; }

        public string ShortName { get; set; }

        public string TableNumber { get; set; }
    }
}