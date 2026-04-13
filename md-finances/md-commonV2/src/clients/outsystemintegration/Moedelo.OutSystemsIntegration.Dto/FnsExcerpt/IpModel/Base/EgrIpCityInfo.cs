using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Base
{
    /// <summary>
    /// Сведенияо о адресообразующем элементе город
    /// </summary>
    [XmlRoot(Namespace = "", IsNullable = true)]
    public class EgrIpCityInfo
    {
        /// <summary>
        /// Тип элемента город (волость и т.п.)
        /// Принимает значение полного наименования типа адресного объекта в соответствии с Классификатором адресов России (КЛАДР)
        /// </summary>
        [XmlAttribute("ТипГород")]
        public string Type { get; set; }
        
        /// <summary>
        /// Наименование (элемент город)
        /// </summary>
        [XmlAttribute("НаимГород")]
        public string Name { get; set; }
    }
}
