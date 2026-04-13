using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Размер доли (в процентах или в виде дроби - десятичной или простой)
    /// </summary>
    public class EgrUlShareSizeInfoDto
    {
        /// <summary>
        /// Размер доли в процентах
        /// </summary>
        public decimal Persent { get; set; }

        /// <summary>
        /// Размер доли в десятичных дробях
        /// </summary>
        public decimal DecimalFraction { get; set; }

        /// <summary>
        /// Размер доли в простых дробях
        /// </summary>
        public EgrUlSimpleFractionTypeDto SimpleFraction { get; set; }
    }
}
