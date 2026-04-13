using System;

namespace Moedelo.Salary.Dto
{
    public class EmploymentHistoryStatementDto
    {
        public int Id { get; set; }

        public int FirmId { get; set; }

        public int WorkerId { get; set; }

        public EhType StatementType { get; set; }

        public DateTime? StatementDate { get; set; }
    }
}
