using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения о правопредшественнике
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlPredecessorInfo : EgrUlGrnDateBaseInfo
    {
        /// <summary>
        /// Сведения о ЮЛ, путем реорганизации которого был создан правопредшественник при реорганизации в форме выделения или разделения с одновременным присоединением или слиянием
        /// </summary>
        [XmlElement("СвЮЛсложнРеорг")]
        public EgrUlCreatedPredecessorInfo ULCreatedPredecessor { get; set; }
        
        /// <summary>
        /// Основной государственный регистрационный номер юридического лица
        /// </summary>
        [XmlAttribute("ОГРН")]
        public string Ogrn { get; set; }

        /// <summary>
        /// ИНН юридического лица
        /// </summary>
        [XmlAttribute("ИНН")]
        public string Inn { get; set; }

        /// <summary>
        /// Полное наименование юридического лица
        /// </summary>
        [XmlAttribute("НаимЮЛПолн")]
        public string FullName { get; set; }
    }
}
