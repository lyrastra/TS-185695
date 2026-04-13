using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base
{
    /// <summary>
    /// Сведения о наименовании и (при наличии) ИНН и ОГРН ЮЛ - учредителя (участника), управляющей организации, залогодержателя, управляющего долей участника, внесенные в ЕГРЮЛ
    /// </summary>
    [XmlRoot(Namespace = "", IsNullable = true)]
    public class EgrUlBaseInfo : EgrUlGrnDateBaseInfo
    {
        /// <summary>
        /// ИНН юридического лица
        /// </summary>
        [XmlAttribute("ИНН")]
        public string Inn { get; set; }

        /// <summary>
        /// Основной государственный регистрационный номер юридического лица
        /// </summary>
        [XmlAttribute("ОГРН")]
        public string Ogrn { get; set; }

        /// <summary>
        /// Полное наименование юридического лица
        /// </summary>
        [XmlAttribute("НаимЮЛПолн")]
        public string FullName { get; set; }
    }
}
