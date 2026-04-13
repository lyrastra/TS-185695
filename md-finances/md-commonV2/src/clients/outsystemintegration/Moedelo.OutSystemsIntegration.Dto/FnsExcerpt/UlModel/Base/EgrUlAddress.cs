using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base
{
    /// <summary>
    /// Сведения об адресе в РФ, внесенные в ЕГРЮЛ
    /// </summary>
    [XmlRoot(Namespace = "", IsNullable = true)]
    public class EgrUlAddress : EgrUlGrnDateBaseInfo
    {
        /// <summary>
        /// Индекс
        /// </summary>
        [XmlAttribute("Индекс")]
        public string Index { get; set; }
        
        /// <summary>
        /// Код субъекта Российской Федерации
        /// </summary>
        [XmlAttribute("КодРегион")]
        public string RegionCode { get; set; }

        /// <summary>
        /// Код адреса по КЛАДР
        /// </summary>
        [XmlAttribute("КодАдрКладр")]
        public string KladrCode { get; set; }

        /// <summary>
        /// Дом (владение и т.п.)
        /// </summary>
        [XmlAttribute("Дом")]
        public string Home { get; set; }

        /// <summary>
        /// Корпус (строение и т.п.)
        /// </summary>
        [XmlAttribute("Корпус")]
        public string Housing { get; set; }

        /// <summary>
        /// Квартира (офис и т.п.)
        /// </summary>
        [XmlAttribute("Кварт")]
        public string Office { get; set; }

        /// <summary>
        /// Субъект Российской Федерации
        /// </summary>
        [XmlElement("Регион")]
        public EgrUlRegionInfo RegionInfo { get; set; }

        /// <summary>
        /// Район (улус и т.п.)
        /// </summary>
        [XmlElement("Район")]
        public EgrUlAreaInfo Area { get; set; }

        /// <summary>
        /// Город (волость и т.п.)
        /// </summary>
        [XmlElement("Город")]
        public EgrUlCityInfo City { get; set; }

        /// <summary>
        /// Населенный пункт (село и т.п.)
        /// </summary>
        [XmlElement("НаселПункт")]
        public EgrUlLocalityInfo Locality { get; set; }

        /// <summary>
        /// Улица (проспект, переулок и т.д.)
        /// </summary>
        [XmlElement("Улица")]
        public EgrUlStreetInfo Street { get; set; }
    }
}
