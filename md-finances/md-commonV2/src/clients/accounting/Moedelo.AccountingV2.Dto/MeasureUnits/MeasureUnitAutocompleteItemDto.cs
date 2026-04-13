namespace Moedelo.AccountingV2.Dto.MeasureUnits
{
    public class MeasureUnitAutocompleteItemDto
    {
        /// <summary>
        /// Условное обозначение ед. измерения
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// Код ед. измерения
        /// Для пользовательских ед. изм. отсутствует
        /// </summary>
        public string Code { get; set; }
        
        /// <summary>
        /// Полное наименоване ед. измерения
        /// Для пользовательских ед. изм. отсутствует
        /// </summary>
        public string Name { get; set; }
    }
}