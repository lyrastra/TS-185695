using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения о ЮЛ, которое было создано в форме слияния с участием правопреемника, или к которому присоединился правопреемник при реорганизации в форме выделения или разделения с одновременным присоединением или слиянием
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlCreatedAssigneeInfo
    {
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
