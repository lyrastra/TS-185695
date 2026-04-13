namespace Moedelo.Common.Enums.Enums.OutSystemIntegration.CasebookApi.GetOrganizationInfo
{
    /// <summary>
    /// Использовать ли "нечеткий" поиск по наименованию
    /// </summary>
    public enum SearchMode
    {
        /// <summary>
        /// Поиск только введённой строки (с учётом словоформ)
        /// </summary>
        Strict = 0,

        /// <summary>
        /// Режим исправления опечаток. 
        /// Поиск и введенной строки, и похожих
        /// </summary>
        Fuzzy = 1,

        /// <summary>
        /// Поиск введенной строки, в случае отсутствия результатов - поиск похожих
        /// </summary>
        FuzzyIfNoStrictResults = 2,
    }
}
