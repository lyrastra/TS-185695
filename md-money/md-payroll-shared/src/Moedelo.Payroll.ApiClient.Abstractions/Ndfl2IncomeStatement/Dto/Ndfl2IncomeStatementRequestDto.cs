using System.Collections.Generic;

namespace Moedelo.Payroll.ApiClient.Abstractions.Ndfl2IncomeStatement.Dto
{
    public class Ndfl2IncomeStatementRequestDto
    {
        public IReadOnlyCollection<int> WorkerIds { get; set; } = new List<int>();
        public int Year { get; set; }
    }
}