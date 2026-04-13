using System.Collections.Generic;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения об учредителях (участниках) юридического лица
    /// </summary>
    public class EgrUlFoundersInfoDto
    {
        /// <summary>
        /// Сведения об учредителе (участнике) - российском юридическом лице
        /// </summary>
        public List<EgrUlFounderUlRfInfoDto> FounderUlRf { get; set; }

        /// <summary>
        /// Сведения об учредителе (участнике) - российском юридическом лице
        /// </summary>
        public List<EgrUlFounderUlForeignInfoDto> FounderUlForeign { get; set; }

        /// <summary>
        /// Сведения об учредителе (участнике) - физическом лице
        /// </summary>
        public List<EgrUlFounderFlInfoDto> FounderFl { get; set; }

        /// <summary>
        /// Сведения об учредителе (участнике) - Российской Федерации, субъекте Российской Федерации, муниципальном образовании
        /// </summary>
        public List<EgrUlFounderRfInfoDto> FounderRf { get; set; }

        /// <summary>
        /// Сведения о паевом инвестиционном фонде, в состав имущества которого включена доля в уставном капитале
        /// </summary>
        public List<EgrUlMutualFundInfoDto> FounderMf { get; set; }
    }
}