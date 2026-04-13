using Moedelo.Common.Enums.Enums.Contract;
using System;
using System.Collections.Generic;

namespace Moedelo.ContractsV2.Dto
{
    /// <summary>
    /// Критерии для поиска договоров
    /// Если поле == null, то фильтр по нему не применяется
    /// </summary>
    public class ContractSearchCriteriaDto
    {
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public IReadOnlyCollection<ContractKind> Kinds { get; set; }

        public IReadOnlyCollection<int> KontragentIds { get; set; }

        public IReadOnlyCollection<string> Numbers { get; set; }
    }
}
