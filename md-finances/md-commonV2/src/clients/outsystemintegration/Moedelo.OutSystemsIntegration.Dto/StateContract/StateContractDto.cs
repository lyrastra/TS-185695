using System;

namespace Moedelo.OutSystemsIntegrationV2.Dto.StateContract
{
    public class StateContractDto
    {
        public string Name { get; set; }

        public string Inn { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public string SubjectContract { get; set; }

        public string PerformanceDate { get; set; }

        public string PlacementMethod { get; set; }

        public decimal? Amount { get; set; }
    }
}
