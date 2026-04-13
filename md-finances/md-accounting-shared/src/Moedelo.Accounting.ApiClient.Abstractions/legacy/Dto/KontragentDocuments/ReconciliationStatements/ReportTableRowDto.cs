namespace Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.KontragentDocuments.ReconciliationStatements
{
    /// <summary>
    /// Данные для строки таблицы акта сверки
    /// </summary>
    public class ReportTableRowDto
    {
        /// <summary> Идентификатор документа </summary>
        public long? DocumentId { get; set; }

        /// <summary> Дата документа </summary>
        public string Date { get; set; }

        /// <summary> Номер документа </summary>
        public string DocumentNumber { get; set; }

        /// <summary> Наименование типа документа (например, Акт) </summary>
        public string DocumentTypeName { get; set; }

        /// <summary> Наименование типа операции (например, Продажа) </summary>
        public string OperationTypeName { get; set; }

        /// <summary> Описание проводки</summary>
        public string PostingDescription { get; set; }

        /// <summary> Сумма дебета </summary>
        public decimal Debit { get; set; }

        /// <summary> Сумма кредита </summary>
        public decimal Credit { get; set; }
    }
}