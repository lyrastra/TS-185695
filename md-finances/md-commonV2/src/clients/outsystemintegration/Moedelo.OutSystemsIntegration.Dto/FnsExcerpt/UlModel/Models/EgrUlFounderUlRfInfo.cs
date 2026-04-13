using System.Collections.Generic;
using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения об учредителе (участнике) - российском юридическом лице
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlFounderUlRfInfo
    {
        /// <summary>
        /// ГРН и дата внесения в ЕГРЮЛ сведений о данном лице
        /// </summary>
        [XmlElement("ГРНДатаПерв")]
        public EgrUlGrnDateInfo Grn { get; set; }

        /// <summary>
        /// Сведения о наименовании и (при наличии) ОГРН и ИНН ЮЛ
        /// </summary>
        [XmlElement("НаимИННЮЛ")]
        public EgrUlBaseInfo UlBaseInfo { get; set; }

        /// <summary>
        /// Сведения о регистрации учредителя (участника) до 01.07.2002 г
        /// </summary>
        [XmlElement("СвРегСтарые")]
        public EgrUlOldRegInfo OldRegInfo { get; set; }
        
        /// <summary>
        /// Сведения о доле учредителя (участника)
        /// </summary>
        [XmlElement("ДоляУстКап")]
        public EgrUlCapitalShareInfo CapitalShare { get; set; }

        /// <summary>
        /// Сведения об обременении доли участника
        /// </summary>
        [XmlElement("СвОбрем")]
        public List<EgrUlEncumbranceShareInfo> EncumbranceShare { get; set; }
    }
}
