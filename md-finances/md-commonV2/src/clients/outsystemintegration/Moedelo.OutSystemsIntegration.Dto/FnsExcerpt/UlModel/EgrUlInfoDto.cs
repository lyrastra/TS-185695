using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Moedelo.OutSystemsIntegration.Dto.Dto.FnsExcerpt.UlModel;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Enums;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel
{
    [XmlType(AnonymousType = true)]
    [XmlRoot("СвЮЛ")]
    public class EgrUlInfoDto
    {
        [XmlAttribute(AttributeName = "ИНН")]
        public string Inn { get; set; }

        [XmlAttribute("ОГРН")]
        public string Ogrn { get; set; }

        [XmlAttribute("КПП")]
        public string Kpp { get; set; }

        /// <summary>
        /// Дата формирования сведений из ЕГРЮЛ в отношении юридического лица
        /// </summary>
        [XmlAttribute("ДатаВып", DataType = "date")]
        public DateTime ExtractDate { get; set; }

        /// <summary>
        /// Дата присвоения ОГРН
        /// </summary>
        [XmlAttribute("ДатаОГРН", DataType = "date")]
        public DateTime OgrnDate { get; set; }

        /// <summary>
        /// Код по выбранному классификатору
        /// </summary>
        [XmlAttribute("КодОПФ")]
        public string CodeOpf { get; set; }

        /// <summary>
        /// Полное наименование организационно-правовой формы
        /// </summary>
        [XmlAttribute("ПолнНаимОПФ")]
        public string OpfFullName { get; set; }

        /// <summary>
        /// Код по выбранному классификатору
        /// </summary>
        [XmlAttribute("СпрОПФ")]
        public EgrUlOpfType OpfType { get; set; }

        public bool OpfTypeSpecified { get; set; }

        /// <summary>
        /// Сведения о наименовании юридического лица
        /// </summary>
        [XmlElement(ElementName = "СвНаимЮЛ")]
        public EgrUlNameInfo EgrUlName { get; set; }

        /// <summary>
        /// Сведения об адресе (месте нахождения)
        /// </summary>
        [XmlElement("СвАдресЮЛ")]
        public EgrUlAddressInfo Address { get; set; }

        /// <summary>
        /// Сведения об адресе электронной почты юридического лица
        /// </summary>
        [XmlElement("СвАдрЭлПочты")]
        public EgrUlEmailInfo Email { get; set; }

        /// <summary>
        /// Сведения о регистрации (образовании) юридического лица
        /// </summary>
        [XmlElement("СвОбрЮЛ")]
        public EgrUlRegInfo RegInfo { get; set; }

        /// <summary>
        /// Сведения о регистрирующем органе по месту нахождения юридического лица
        /// </summary>
        [XmlElement("СвРегОрг")]
        public EgrUlRegistrationAgencyInfo RegistrationAgency { get; set; }

        /// <summary>
        /// Сведения о состоянии (статусе) юридического лица
        /// </summary>
        [XmlElement("СвСтатус")]
        public List<EgrUlStatusInfo> UlStatus { get; set; }

        /// <summary>
        /// Сведения о прекращении юридического лица
        /// </summary>
        [XmlElement("СвПрекрЮЛ")]
        public EgrUlTerminationInfo TerminationInfo { get; set; }

        /// <summary>
        /// Сведения об учете в налоговом органе
        /// </summary>
        [XmlElement("СвУчетНО")]
        public EgrUlTaxRegistrationInfo TaxRegistration { get; set; }

        /// <summary>
        /// Сведения о регистрации юридического лица в качестве страхователя в территориальном органе Пенсионного фонда Российской Федерации
        /// </summary>
        [XmlElement("СвРегПФ")]
        public EgrUlPFRegistrationInfo PFRegistration { get; set; }

        /// <summary>
        /// Сведения о регистрации юридического лица в качестве страхователя в исполнительном органе Фонда социального страхования Российской Федерации
        /// </summary>
        [XmlElement("СвРегФСС")]
        public EgrUlFssRegistrationInfo FssRegistration { get; set; }

        /// <summary>
        /// Сведения о размере указанного в учредительных документах коммерческой организации уставного капитала (складочного капитала, уставного фонда, паевого фонда)
        /// </summary>
        [XmlElement("СвУстКап")]
        public EgrUlCapitalInfo Capital { get; set; }

        /// <summary>
        /// Сведения об управляющей организации
        /// </summary>
        [XmlElement("СвУпрОрг")]
        public List<EgrUlManagementCompanyInfo> ManagementCompany { get; set; }

        /// <summary>
        /// Сведения о лице, имеющем право без доверенности действовать от имени юридического лица
        /// </summary>
        [XmlElement("СведДолжнФЛ")]
        public List<EgrUlAuthorizedFLInfo> AuthorizedFl { get; set; }

        /// <summary>
        /// Сведения о лице, имеющем право без доверенности действовать от имени юридического лица
        /// </summary>
        [XmlElement("СвУчредит")]
        public EgrUlFoundersInfo Founders { get; set; }

        /// <summary>
        /// Сведения о доле в уставном капитале общества с ограниченной ответственностью, принадлежащей обществу
        /// </summary>
        [XmlElement("СвДоляООО")]
        public EgrUlCapitalShareInfo CapitalShareOOO { get; set; }

        /// <summary>
        /// Сведения о держателе реестра акционеров акционерного общества
        /// </summary>
        [XmlElement("СвДержРеестрАО")]
        public EgrUlRegistrarAOInfo RegistrarAO { get; set; }

        /// <summary>
        /// Сведения о видах экономической деятельности по Общероссийскому классификатору видов экономической деятельности
        /// </summary>
        [XmlElement("СвОКВЭД")]
        public EgrUlOkvedInfo Okved { get; set; }

        /// <summary>
        /// Сведения о лицензиях, выданных ЮЛ
        /// </summary>
        [XmlElement("СвЛицензия")]
        public List<EgrUlLicenseInfo> License { get; set; }

        /// <summary>
        /// Сведения об обособленных подразделениях юридического лица
        /// </summary>
        [XmlElement("СвПодразд")]
        public EgrUlDivisionsInfo Divisions { get; set; }
        
        /// <summary>
        /// Сведения об участии в реорганизации
        /// </summary>
        [XmlElement("СвРеорг")]
        public List<EgrUlReorgInfo> Reorganization { get; set; }
        
        /// <summary>
        /// Сведения о правопредшественнике
        /// </summary>
        [XmlElement("СвПредш")]
        public List<EgrUlPredecessorInfo> Predecessor { get; set; }
        
        /// <summary>
        /// Сведения о крестьянском (фермерском) хозяйстве, на базе имущества которого создано юридическое лицо
        /// </summary>
        [XmlElement("СвКФХПредш")]
        public List<EgrUlPFEPredecessorInfo> PFEPredecessor { get; set; }
        
        /// <summary>
        /// Сведения о правопреемнике
        /// </summary>
        [XmlElement("СвПреем")]
        public List<EgrUlAssigneeInfo> Assignee { get; set; }
        
        /// <summary>
        /// Сведения о крестьянском (фермерском) хозяйстве, которые  внесены в ЕГРИП в связи с приведением правового статуса крестьянского (фермерского) хозяйства в соответствие с нормами части первой Гражданского кодекса Российской Федерации
        /// </summary>
        [XmlElement("СвКФХПреем")]
        public EgrUlPFEAssigneeInfo PFEAssignee { get; set; }
    }
}
