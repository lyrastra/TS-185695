using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения о представительствах юридического лица
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlAgencyInfo
    {
        /// <summary>
        /// ГРН и дата внесения в ЕГРЮЛ сведений о данном филиале
        /// </summary>
        [XmlElement("ГРНДатаПерв")]
        public EgrUlGrnDateInfo Grn { get; set; }

        /// <summary>
        /// Сведения о наименовании представительства
        /// </summary>
        [XmlElement("СвНаим")]
        public EgrUlAffiliateNameInfo Name { get; set; }

        /// <summary>
        /// Адрес (место расположения) на территории Российской Федерации
        /// </summary>
        [XmlElement("АдрМНРФ")]
        public EgrUlAddress AddressRF { get; set; }

        /// <summary>
        /// Адрес (место расположения) за пределами территории Российской Федерации
        /// </summary>
        [XmlElement("АдрМНИн")]
        public EgrUlForeignAddress AddressForeign { get; set; }

        /// <summary>
        /// Сведения об учете в налоговом органе по месту нахождения представительства
        /// </summary>
        [XmlElement("СвУчетНОПредстав")]
        public EgrUlTaxRegAffiliateInfo TaxRegAffiliate { get; set; }
    }
}
