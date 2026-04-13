using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения о крестьянском (фермерском) хозяйстве, на базе имущества которого создано юридическое лицо
    /// </summary>
    public class EgrUlPFEPredecessorInfoDto
    {
        /// <summary>
        /// Сведения о ФИО и (при наличии) ИНН главы КФХ
        /// </summary>
        public EgrUlFlInfoDto FlInfo { get; set; }
        
        /// <summary>
        /// ОГРНИП крестьянского (фермерского) хозяйства
        /// </summary>
        public string Ogrn { get; set; }
    }
}
