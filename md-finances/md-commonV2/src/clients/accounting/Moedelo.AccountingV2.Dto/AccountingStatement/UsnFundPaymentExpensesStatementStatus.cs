namespace Moedelo.AccountingV2.Dto.AccountingStatement
{
    /// <summary>
    /// Статус бух.справки с расходами по ФВ и ДВ
    /// </summary>
    public enum UsnFundPaymentExpensesStatementStatus
    {
        /// <summary> Не поддерживается </summary>
        NotSupported = 0,

        /// <summary> Не создана </summary>
        NotCreated = 1,

        /// <summary> Создана </summary>
        Created = 2,

        /// <summary> Создана, но на данный момент не поддерживается </summary>
        CreatedButNotSupported = 3
    }
}
