using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base
{
    /// <summary>
    /// Доля в уставном капитале (складочном капитале, уставном фонде, паевом фонде), внесенная в ЕГРЮЛ
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlCapitalShareInfo : EgrUlGrnDateBaseInfo
    {
        /// <summary>
        /// Размер доли (в процентах или в виде дроби - десятичной или простой)
        /// </summary>
        [XmlElement("РазмерДоли")]
        public EgrUlShareSizeInfo Size { get; set; }
        
        /// <summary>
        /// Номинальная стоимость доли в рублях
        /// </summary>
        [XmlAttribute("НоминСтоим")]
        public decimal Sum { get; set; }
    }
}
