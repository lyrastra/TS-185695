namespace Moedelo.TaxPostings.Dto
{
    public class OsnoNormalizedSumForQuarterDto
    {
        /// <summary>
        /// Идентификатор проводки
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Принятый нормированный расход за 1-й квартал
        /// </summary>
        public decimal? NormalizedFirstQuarter { get; set; }

        /// <summary>
        /// Принятый нормированный расход за 2-й квартал
        /// </summary>
        public decimal? NormalizedSecondQuarter { get; set; }

        /// <summary>
        /// Принятый нормированный расход за 3-й квартал
        /// </summary>
        public decimal? NormalizedThirdQuarter { get; set; }

        /// <summary>
        /// Принятый нормированный расход за 4-й квартал
        /// </summary>
        public decimal? NormalizedForthQuarter { get; set; }
    }
}