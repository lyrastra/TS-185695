using System.Collections.Generic;
using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения об адресе (месте нахождения)
    /// </summary>
    public class EgrUlAddressInfoDto
    {
        /// <summary>
        /// Адрес (место нахождения) юридического лица
        /// </summary>
        public EgrUlAddressDto Address { get; set; }

        /// <summary>
        /// Сведения о недостоверности адреса или отсутствии связи с ЮЛ по указанному адресу
        /// </summary>
        public List<EgrUlSignLackAddressInfoDto> InfoAboutUnreliabilityAddress { get; set; }
    }
}