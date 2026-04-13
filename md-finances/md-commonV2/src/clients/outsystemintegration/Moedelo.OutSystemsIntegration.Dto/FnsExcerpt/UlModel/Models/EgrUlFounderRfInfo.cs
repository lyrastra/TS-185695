using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения об учредителе (участнике) - Российской Федерации, субъекте Российской Федерации, муниципальном образовании
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlFounderStInfo
    {
        /// <summary>
        /// ГРН и дата внесения в ЕГРЮЛ сведений о данном лице
        /// </summary>
        [XmlElement("ГРНДатаПерв")]
        public EgrUlGrnDateInfo Grn { get; set; }

        /// <summary>
        /// Сведения о виде учредителя (участника) и (при необходимости) наименовании муниципального образования и региона
        /// </summary>
        [XmlElement("ВидНаимУчр")]
        public EgrUlFounderTypeInfo FounderTypeInfo { get; set; }

        /// <summary>
        /// Сведения о доле учредителя (участника)
        /// </summary>
        [XmlElement("ДоляУстКап")]
        public EgrUlCapitalShareInfo CapitalShare { get; set; }

        /// <summary>
        /// Сведения об органе государственной власти, органе местного самоуправления или о юридическом лице, осуществляющем права учредителя (участника)
        /// </summary>
        [XmlElement("СвОргОсущПр")]
        public EgrUlStExerciseFounderRightsInfo StExerciseFounderRights { get; set; }

        /// <summary>
        /// Сведения о физическом лице, осуществляющем права учредителя (участника)
        /// </summary>
        [XmlElement("СвФЛОсущПр")]
        public EgrUlFlExerciseFounderRightsInfo FlExerciseFounderRights { get; set; }

        /// <summary>
        /// Сведения об обременении доли участника
        /// </summary>
        [XmlElement("СвОбрем")]
        public EgrUlEncumbranceShareInfo EncumbranceShare { get; set; }
    }
}
