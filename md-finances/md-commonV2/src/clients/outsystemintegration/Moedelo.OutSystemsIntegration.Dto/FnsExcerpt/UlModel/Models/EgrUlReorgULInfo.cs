using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Enums;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения о юридических лицах, участвующих  в реорганизации
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlReorgULInfo
    {
        /// <summary>
        /// ГРН и дата внесения в ЕГРЮЛ записи об исправлении технической ошибки в указанных сведениях
        /// </summary>
        [XmlElement("ГРНДатаИспр")]
        public EgrUlGrnDateInfo Grn { get; set; }
        
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
        public string ULFullName { get; set; }

        /// <summary>
        /// Состояние юридического лица после завершения реорганизации
        /// </summary>
        [XmlAttribute("СостЮЛпосле")]
        public EgrUlStatuAfterReorg ULStatuAfterReorg { get; set; }

        /// <summary>
        /// Наличие ULStatuAfterReorg
        /// </summary>
        [XmlIgnore]
        public bool ULStatuAfterReorgSpecified { get; set; }
    }
}
