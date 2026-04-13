using System.Collections.Generic;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.KontragentDocuments.ReconciliationStatements
{
    /// <summary>
    /// Данные для таблицы акта сверки
    /// </summary>
    public class ReportTableCalculationDataDto
    {
        /// <summary>Данные о договоре</summary>
        public ContractDto Contract { get; set; }
        
        /// <summary>Сальдо на начало периода</summary>
        public ReportTableRowDto InitialBalanceRow { get; set; }

        /// <summary>Обороты</summary>
        public ReportTableRowDto TurnoverRow { get; set; }

        /// <summary>Сальдо на конец периода</summary>
        public ReportTableRowDto FinalBalanceRow { get; set; }

        public List<ReportTableRowDto> Rows { get; set; }
    }
}