using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения о наименовании юридического лица
    /// </summary>
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = true)]
    public class EgrUlNameInfo : EgrUlGrnDateBaseInfo
    {
        /// <summary>
        /// Полное наименование юридического лица на русском языке
        /// </summary>
        [XmlAttribute("НаимЮЛПолн")]
        public string FullName { get; set; }

        /// <summary>
        /// Сокращенное наименование юридического лица на русском языке
        /// </summary>
        [XmlAttribute("НаимЮЛСокр")]
        public string ShortName { get; set; }
    }
}
