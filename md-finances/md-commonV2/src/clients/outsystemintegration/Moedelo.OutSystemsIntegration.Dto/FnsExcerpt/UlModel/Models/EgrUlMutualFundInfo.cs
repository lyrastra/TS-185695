using System.Collections.Generic;
using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения о паевом инвестиционном фонде, в состав имущества которого включена доля в уставном капитале
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlMutualFundInfo
    {
        /// <summary>
        /// ГРН и дата внесения в ЕГРЮЛ сведений о данном лице
        /// </summary>
        [XmlElement("ГРНДатаПерв")]
        public EgrUlGrnDateInfo Grn { get; set; }

        /// <summary>
        /// Сведения о названии (индивидуальном обозначении) паевого инвестиционного фонда
        /// </summary>
        [XmlElement("СвНаимПИФ")]
        public EgrUlMutualFundNameInfo UlInfo { get; set; }
        
        /// <summary>
        /// Сведения об управляющей компании паевого инвестиционного фонда
        /// </summary>
        [XmlElement("СвУпрКомпПИФ")]
        public EgrUlManagementCompanyMutualFundInfo ManagementCompany { get; set; }

        /// <summary>
        /// Сведения о доле учредителя (участника)
        /// </summary>
        [XmlElement("ДоляУстКап")]
        public EgrUlCapitalShareInfo CapitalShare { get; set; }

        /// <summary>
        /// Сведения об обременении доли участника
        /// </summary>
        [XmlElement("СвОбрем")]
        public List<EgrUlEncumbranceShareInfo> EncumbranceShare { get; set; }
    }
}
