namespace Moedelo.Edm.Dto.TsWizard
{
    /// <summary>
    /// Часть хинта (хинты - части решений)
    /// </summary>
    public class HintPartDto
    {
        /// <summary>
        /// Должна-ли у этой части быть кнопка "Копировать"
        /// </summary>
        public bool IsCopyEnabled { get; set; }

        /// <summary>
        /// Должна-ли у эта часть быть выделена тегом h2
        /// </summary>
        public bool IsH2Styled { get; set; }

        /// <summary>
        /// Текст этой части хинта
        /// </summary>
        public string Text { get; set; }
    }
}
