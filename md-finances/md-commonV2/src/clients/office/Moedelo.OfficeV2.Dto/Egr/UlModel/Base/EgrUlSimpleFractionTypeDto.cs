namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Base
{
    /// <summary>
    /// Представление простой дроби
    /// </summary>
    public class EgrUlSimpleFractionTypeDto
    {
        /// <summary>
        /// Числитель простой дроби
        /// </summary>
        public string Numerator { get; set; }

        /// <summary>
        /// Знаменатель простой дроби
        /// </summary>
        public string Denominator { get; set; }
    }
}