namespace Moedelo.AccountingV2.Dto.MeasureUnits
{
    public class MeasureUnitsAutocompleteRequestDto
    {
        /// <summary>
        /// Подстрока, которая содержится в усл. обозначении/названии/коде 
        /// </summary>
        public string Query { get; set; } = string.Empty;

        /// <summary>
        /// Количество объектов, которое нужно получить 
        /// </summary>
        public uint Count { get; set; } = 5;
    }
}