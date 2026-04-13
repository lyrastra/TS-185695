using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;

// ReSharper disable once CheckNamespace
namespace Moedelo.OutSystemsIntegration.Dto.Dto.FnsExcerpt.UlModel
{
    [XmlType(AnonymousType = true)]
    public class EgrUlRegistrarAOInfo
    {
        /// <summary>
        /// ГРН и дата внесения в ЕГРЮЛ сведений о данном лице
        /// </summary>
        [XmlElement("ГРНДатаПерв")]
        public EgrUlGrnDateInfo Grn { get; set; }

        /// <summary>
        /// Наименование и (при наличии) ОГРН и ИНН держателе реестра акционеров акционерного общества
        /// </summary>
        [XmlAttribute("ДержРеестрАО")]
        public string RegistrarAO { get; set; }
    }
}
