namespace Moedelo.Common.Enums.Enums.Kontragents.Arbitration
{
    /// <summary>
    /// Состояние арбитражного дела (CaseBookApi)
    /// </summary>
    public enum CaseResultCode
    {
        /// <summary>
        ///  "Неизвестно"
        /// </summary>
        Unknown = -1,

        /// <summary>
        ///  "Иск удовлетворен"
        /// </summary>
        Satisfied = 0,

        /// <summary>
        ///  "Иск удовлетворен частично"
        /// </summary>
        SatisfiedPartially = 1,

        /// <summary>
        ///  "Иск не удовлетворен"
        /// </summary>
        NotSatisfied = 2,

        /// <summary>
        ///  "Заявление/жалоба возвращена"
        /// </summary>
        Returned = 3
    }
}