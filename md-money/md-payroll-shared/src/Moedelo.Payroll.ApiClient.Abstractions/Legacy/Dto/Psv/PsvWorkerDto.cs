using Moedelo.Payroll.Enums.Worker;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Psv
{
    public class PsvWorkerDto
    {
        public int Id { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public string Snils { get; set; }

        public string Inn { get; set; }

        public bool IsForeigner { get; set; }

        public bool IsExpert { get; set; }

        public WorkerForeignerStatus? ForeignerStatus { get; set; }
    }
}