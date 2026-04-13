using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base
{
    /// <summary>
    /// Сведения о ФИО и (при наличии) ИНН ФЛ
    /// </summary>
    [XmlRoot(Namespace = "", IsNullable = true)]
    public class EgrUlFlInfo : EgrUlGrnDateBaseInfo
    {
        /// <summary>
        /// Фамилия
        /// </summary>
        [XmlAttribute("Фамилия")]
        public string Surname { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [XmlAttribute("Имя")]
        public string Name { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        [XmlAttribute("Отчество")]
        public string Patronymic { get; set; }

        /// <summary>
        /// ИНН ФЛ
        /// </summary>
        [XmlAttribute("ИННФЛ")]
        public string InnFl { get; set; }
    }
}
