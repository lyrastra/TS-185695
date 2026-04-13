using Moedelo.Accounting.Enums;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.PurseOperation
{
    /// <summary>
    /// Модель для сохранения операции по электронным деньгам
    /// </summary>
    public class PurseOperationForMultipleTypesDto
    {
        public string Date { get; set; }

        public int PurseId { get; set; }
        /// <summary>
        /// ID контрагента, который связан с населением
        /// </summary>
        public int? KontragentId { get; set; }

        /// <summary>
        /// ID контрагента, который связан с кошельком
        /// </summary>

        public string Comment { get; set; }

        public decimal Sum { get; set; }

        public PurseOperationType PurseOperationType { get; set; }

        public int SettlementAccountId { get; set; }

        public TaxationSystemType? TaxationSystemType { get; set; }
        
        /// <summary>
        /// В том числе НДС
        /// </summary>
        public bool? IncludeNds { get; set; }

        /// <summary>
        /// Тип НДС
        /// </summary>
        public AccountingNdsType? NdsType { get; set; }

        /// <summary>
        /// Сумма НДС
        /// </summary>
        public decimal? NdsSum { get; set; }
    }
}
