using Moedelo.Accounting.Enums;
using Moedelo.Accounting.Enums.Documents;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.AdvanceStatements
{
    public class AdvanceStatementBusinessTripItemDto
    {
        public long Id { get; set; }

        public BusinessTripExpensesType Type { get; set; }

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

        /// <summary>
        /// Учесть в (переключатель СНО)
        /// </summary>
        public TaxationSystemType TaxationSystemType { get; set; }

        public int? KontragentId { get; set; }
    }
}
