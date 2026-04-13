using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    [XmlType(AnonymousType = true)]
    public class EgrUlStatusInfo : EgrUlGrnDateBaseInfo
    {
        /// <summary>
        /// Сведения о правоспособности (статусе) юридического лица
        /// </summary>
        [XmlElement("СвСтатус")]
        public EgrUlStatus Status { get; set; }

        /// <summary>
        /// Сведения о решении о предстоящем исключении недействующего ЮЛ из ЕГРЮЛ и его публикации
        /// </summary>
        [XmlElement("СвРешИсклЮЛ")]
        public EgrUlDecisionToExcludeInfo DecisionToExclude { get; set; }
    }
}
