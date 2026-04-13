using System.Collections.Generic;
using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения об участии в реорганизации
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlReorgInfo
    {
        /// <summary>
        /// Сведения о форме реорганизации (статусе) юридического лица
        /// </summary>
        [XmlElement("СвСтатус")]
        public EgrUlReorgStatusInfo Status { get; set; }
        
        /// <summary>
        /// ГРН и дата внесения в ЕГРЮЛ записи, содержащей сведения о начале реорганизации
        /// </summary>
        [XmlElement("ГРНДата")]
        public EgrUlGrnDateInfo Grn { get; set; }

        /// <summary>
        /// ГРН и дата внесения записи, которой в ЕГРЮЛ внесены сведения об изменении состава участвующих в реорганизации юридических лиц
        /// </summary>
        [XmlElement("ГРНДатаИзмСостРеоргЮЛ")]
        public EgrUlGrnDateInfo GrnChangeRearg { get; set; }

        /// <summary>
        /// Сведения о юридических лицах, участвующих  в реорганизации
        /// </summary>
        [XmlElement("СвРеоргЮЛ")]
        public List<EgrUlReorgULInfo> ReargUL { get; set; }
    }
}
