using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения об управляющей компании паевого инвестиционного фонда
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlManagementCompanyMutualFundInfo
    {
        /// <summary>
        /// ГРН и дата внесения в ЕГРЮЛ сведений о данном лице
        /// </summary>
        [XmlElement("ГРНДатаПерв")]
        public EgrUlGrnDateInfo Grn { get; set; }

        /// <summary>
        /// СНаименование и (при наличии) ОГРН и ИНН управляющей компании паевого инвестиционного фонда
        /// </summary>
        [XmlElement("УпрКомпПиф")]
        public EgrUlBaseInfo ManagementCompany { get; set; }
    }
}
