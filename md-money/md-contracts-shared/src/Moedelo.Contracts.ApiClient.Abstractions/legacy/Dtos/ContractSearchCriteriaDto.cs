using System;
using System.Collections.Generic;
using Moedelo.Contracts.Enums;

namespace Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos
{
    public class ContractSearchCriteriaDto
    {
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public IReadOnlyCollection<ContractKind> Kinds { get; set; }

        public IReadOnlyCollection<int> KontragentIds { get; set; }

        public IReadOnlyCollection<string> Numbers { get; set; }
    }
}