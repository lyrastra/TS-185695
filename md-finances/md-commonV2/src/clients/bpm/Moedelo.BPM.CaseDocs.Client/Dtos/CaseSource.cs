namespace Moedelo.BPM.CaseDocs.Client.Dtos
{
    /// <summary>
    ///     Тип источника создания обращения
    /// </summary>
    public enum CaseSource
    {
        /// <summary>
        ///     Неизвестно
        /// </summary>
        Undefined,

        /// <summary>
        ///     Апи
        /// </summary>
        Api,

        /// <summary>
        ///     Чат с бизнес-асистентом
        /// </summary>
        Chat,

        /// <summary>
        ///     Из письма
        /// </summary>
        Email,

        /// <summary>
        ///     Создано сотрудником Моего Дела
        /// </summary>
        Employee
    }
}