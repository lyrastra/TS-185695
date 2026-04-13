using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения о наименовании филиала
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlAffiliateNameInfo : EgrUlGrnDateBaseInfo
    {
        /// <summary>
        /// ГРН и дата внесения в ЕГРЮЛ сведений о данном филиале
        /// </summary>
        [XmlAttribute("ГРНДатаПерв")]
        public string Name { get; set; }
    }
}
