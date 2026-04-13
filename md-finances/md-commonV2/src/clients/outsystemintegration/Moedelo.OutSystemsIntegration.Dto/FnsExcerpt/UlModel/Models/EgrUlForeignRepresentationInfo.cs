using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения о наименовании представительства или филиала в Российской Федерации, через которое иностранное ЮЛ осуществляет полномочия управляющей организации
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlForeignRepresentationInfo : EgrUlGrnDateBaseInfo
    {
        /// <summary>
        /// Полное наименование представительства или филиала в Российской Федерации, через которое иностранное ЮЛ осуществляет полномочия управляющей организации
        /// </summary>
        [XmlAttribute("НаимПредЮЛ")]
        public string Name { get; set; }
    }
}
