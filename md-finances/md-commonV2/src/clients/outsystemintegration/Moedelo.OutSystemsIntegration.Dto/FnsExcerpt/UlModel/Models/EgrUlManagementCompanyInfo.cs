using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения об управляющей организации
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlManagementCompanyInfo
    {
        /// <summary>
        /// ГРН и дата внесения в ЕГРЮЛ сведений о данном лице
        /// </summary>
        [XmlElement("ГРНДатаПерв")]
        public EgrUlGrnDateInfo Grn { get; set; }

        /// <summary>
        /// Сведения о наименовании и (при наличии) ОГРН и ИНН ЮЛ - управляющей организации
        /// </summary>
        [XmlElement("НаимИННЮЛ")]
        public EgrUlBaseInfo Name { get; set; }

        /// <summary>
        /// Сведения о регистрации в стране происхождения
        /// </summary>
        [XmlElement("СвРегИн")]
        public EgrUlForeignRegUlInfo ForeignRegUlInfo { get; set; }

        /// <summary>
        /// Сведения о наименовании представительства или филиала в Российской Федерации, через которое иностранное ЮЛ осуществляет полномочия управляющей организации
        /// </summary>
        [XmlElement("СвПредЮЛ")]
        public EgrUlForeignRepresentationInfo ForeignRepresentation { get; set; }

        /// <summary>
        /// Сведения об адресе управляющей организации в Российской Федерации
        /// </summary>
        [XmlElement("СвАдрРФ")]
        public EgrUlAddress RfAddress { get; set; }

        /// <summary>
        /// Сведения о контактном телефоне
        /// </summary>
        [XmlElement("СвНомТел")]
        public EgrUlPhoneInfo Phone { get; set; }

        /// <summary>
        /// Сведения о лице, через которое иностранное юридическое лицо осуществляет полномочия управляющей организации
        /// </summary>
        [XmlElement("ПредИнЮЛ")]
        public EgrUlRepresentativeInfo Representative { get; set; }
    }
}
