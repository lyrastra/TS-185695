using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения об органе государственной власти, органе местного самоуправления или о юридическом лице, осуществляющем права учредителя (участника)
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlStExerciseFounderRightsInfo
    {
        /// <summary>
        /// ГРН и дата внесения в ЕГРЮЛ сведений о данном лице
        /// </summary>
        [XmlElement("ГРНДатаПерв")]
        public EgrUlGrnDateInfo Grn{ get; set; }

        /// <summary>
        /// Сведения о наименовании и (при наличии) ОГРН и ИНН органа государственной власти, органа местного самоуправления или ЮЛ
        /// </summary>
        [XmlElement("НаимИННЮЛ")]
        public EgrUlBaseInfo AuthorityInfo { get; set; }
    }
}
