using System;
using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения о нотариальном удостоверении договора залога
    /// </summary>
    [XmlRoot(Namespace = "", IsNullable = true)]
    public class EgrUlNotarizationPledgeAgreementInfo
    {
        /// <summary>
        /// ФИО и (при наличии) ИНН нотариуса, удостоверившего договор залога
        /// </summary>
        [XmlElement("СвНотариус")]
        public EgrUlFlInfo Notary { get; set; }

        /// <summary>
        /// Номер договора залога
        /// </summary>
        [XmlElement("Номер")]
        public string Number { get; set; }

        /// <summary>
        /// Дата договора залога
        /// </summary>
        [XmlAttribute("Дата", DataType = "date")]
        public DateTime Date { get; set; }
    }
}
