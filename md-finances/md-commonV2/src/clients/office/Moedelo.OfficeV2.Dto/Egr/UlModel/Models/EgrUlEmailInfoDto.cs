using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения об адресе электронной почты юридического лица
    /// </summary>
    public class EgrUlEmailInfoDto : EgrUlGrnDateBaseInfoDto
    {
        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        public string Email { get; set; }
    }
}