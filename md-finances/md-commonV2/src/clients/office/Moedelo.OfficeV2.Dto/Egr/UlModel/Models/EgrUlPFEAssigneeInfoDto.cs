using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения о крестьянском (фермерском) хозяйстве, которые  внесены в ЕГРИП в связи с приведением правового статуса крестьянского (фермерского) хозяйства в соответствие с нормами части первой Гражданского кодекса Российской Федерации
    /// </summary>
    public class EgrUlPFEAssigneeInfoDto
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
