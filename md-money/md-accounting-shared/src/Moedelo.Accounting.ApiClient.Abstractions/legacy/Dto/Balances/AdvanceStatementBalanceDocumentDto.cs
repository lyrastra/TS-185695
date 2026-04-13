namespace Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.Balances
{
    public class AdvanceStatementBalanceDocumentDto
    {
        /// <summary>
        /// Базовый идентификатор документа 
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Номер документа
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Сумма документа (долг сотрудника)
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Субконто сотрудника (чей долг) 
        /// </summary>
        public long SubcontoId { get; set; }

        /// <summary>
        /// Ссылка на AccountingBalanceItem, к которому привязан документ
        /// </summary>
        public long AccountingBalanceItemId { get; set; }
    }
}