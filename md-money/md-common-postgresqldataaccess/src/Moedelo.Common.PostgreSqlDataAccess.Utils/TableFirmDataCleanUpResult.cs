namespace Moedelo.Common.PostgreSqlDataAccess.Utils
{
    /// <summary>
    /// результат зачистки данных фирм по одной из таблиц
    /// </summary>
    public class TableFirmDataCleanUpResult
    {
        /// <summary>
        /// название таблицы
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// количество удалённых строк
        /// </summary>
        public int DeletedRowCount { get; set; }
        /// <summary>
        /// признак того, что в процессе удаления произошла ошибка
        /// </summary>
        public bool IsError { get; set; }
        /// <summary>
        /// статусное сообщение или текст ошибки
        /// </summary>
        public string Message { get; set; }
    }
}
