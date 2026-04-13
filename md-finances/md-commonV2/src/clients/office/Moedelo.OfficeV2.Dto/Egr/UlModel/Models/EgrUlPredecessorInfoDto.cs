using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения о правопредшественнике
    /// </summary>
    public class EgrUlPredecessorInfoDto : EgrUlGrnDateBaseInfoDto
    {
        /// <summary>
        /// Сведения о ЮЛ, путем реорганизации которого был создан правопредшественник при реорганизации в форме выделения или разделения с одновременным присоединением или слиянием
        /// </summary>
        public EgrUlCreatedPredecessorInfoDto UlCreatedPredecessor { get; set; }
        
        /// <summary>
        /// Основной государственный регистрационный номер юридического лица
        /// </summary>
        public string Ogrn { get; set; }

        /// <summary>
        /// ИНН юридического лица
        /// </summary>
        public string Inn { get; set; }

        /// <summary>
        /// Полное наименование юридического лица
        /// </summary>
        public string FullName { get; set; }
    }
}