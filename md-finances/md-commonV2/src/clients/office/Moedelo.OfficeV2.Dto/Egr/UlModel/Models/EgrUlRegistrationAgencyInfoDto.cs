using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения о регистрирующем органе по месту нахождения юридического лица
    /// </summary>
    public class EgrUlRegistrationAgencyInfoDto : EgrUlGrnDateBaseInfoDto
    {
        /// <summary>
        /// Код органа по справочнику СОНО
        /// </summary>
        public string Code { get; set; }
        
        /// <summary>
        /// Наименование регистрирующего (налогового) органа
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Адрес регистрирующего органа
        /// </summary>
        public string Address { get; set; }
    }
}