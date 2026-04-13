using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Documents;
using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.AdvanceStatement
{
    public class AdvanceStatementProductAndMaterialItemDto
    {
        public long Id { get; set; }

        public AdvanceStatementItemDataType ExpenditureType { get; set; }

        /// <summary>
        /// Сумма (отчет)
        /// </summary>
        public decimal ReportedSum { get; set; }

        /// <summary>
        /// Сумма (принято)
        /// </summary>
        public decimal AcceptedSum { get; set; }

        /// <summary>
        /// Сумма НДС
        /// </summary>
        public decimal? NdsSum { get; set; }

        public int? KontragentId { get; set; }

        public IReadOnlyCollection<AdvanceStatementExpenditureProductAndMaterialItemDto> SubItems { get; set; }

        public TaxationSystemType TaxationSystemType { get; set; }
    }
}
