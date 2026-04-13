using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения о должности ФЛ
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlFlPositionInfo : EgrUlGrnDateBaseInfo
    {
        /// <summary>
        /// Основной государственный регистрационный номер индивидуального предпринимателя - управляющего юридическим лицом
        /// </summary>
        [XmlAttribute("ОГРНИП")]
        public string OgrnIp { get; set; }

        /// <summary>
        /// Вид должностного лица по справочнику СКФЛЮЛ (указывается код по справочнику)
        /// </summary>
        [XmlAttribute("ВидДолжн")]
        public string ExecutiveType { get; set; }

        /// <summary>
        /// Наименование вида должностного лица по справочнику СКФЛЮЛ
        /// </summary>
        [XmlAttribute("НаимВидДолжн")]
        public string ExecutiveTypeName { get; set; }

        /// <summary>
        /// Наименование должности
        /// </summary>
        [XmlAttribute("НаимДолжн")]
        public string PositionName { get; set; }
    }
}
