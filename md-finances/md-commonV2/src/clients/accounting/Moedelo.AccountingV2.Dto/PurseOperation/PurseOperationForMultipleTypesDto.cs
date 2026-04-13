using Moedelo.Common.Enums.Enums.Accounting;
using OperationType = Moedelo.Common.Enums.Enums.PostingEngine.OperationType;

namespace Moedelo.AccountingV2.Dto.PurseOperation
{
    /// <summary>
    /// Модель для сохранения операции по электронным деньгам
    /// </summary>
    public class PurseOperationForMultipleTypesDto
    {
        /// <summary>
        /// Идентификатор контрагента
        /// </summary>
        public int? KontragentId { get; set; }

        /// <summary>
        /// Идентификатор договора
        /// </summary>
        public long? ContractBaseId { get; set; }

        public string Number { get; set; }

        public string Comment { get; set; }

        public string Date { get; set; }

        public decimal Sum { get; set; }

        public decimal? UsnSum { get; set; }

        public PurseOperationType PurseOperationType { get; set; }

        public OperationType OperationType { get; set; }

        public int PurseId { get; set; }

        public int SettlementAccountId { get; set; }

        public long? BillDocumentBaseId { get; set; }
        
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
