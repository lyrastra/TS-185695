using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения о наименовании юридического лица
    /// </summary>
    public class EgrUlNameInfoDto : EgrUlGrnDateBaseInfoDto
    {
        /// <summary>
        /// Полное наименование юридического лица на русском языке
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Сокращенное наименование юридического лица на русском языке
        /// </summary>
        public string ShortName { get; set; }
    }
}
