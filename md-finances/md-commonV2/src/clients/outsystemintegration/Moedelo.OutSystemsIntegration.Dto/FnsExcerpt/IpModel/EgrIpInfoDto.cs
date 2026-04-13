using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Enums;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Models;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel
{
    [XmlType(AnonymousType = true)]
    [XmlRoot("СвИП")]
    public class EgrIpInfoDto
    {
        [XmlAttribute("ИННФЛ")]
        public string InnFl { get; set; }

        [XmlAttribute("ОГРНИП")]
        public string OgrnIp { get; set; }

        [XmlAttribute("ДатаВып", DataType = "date")]
        public DateTime ExtractDate { get; set; }

        [XmlAttribute("ДатаОГРНИП", DataType = "date")]
        public DateTime OgrnDate { get; set; }

        [XmlAttribute("КодВидИП")]
        public EgrIpType IpType { get; set; }

        [XmlAttribute("НаимВидИП")]
        public string IpTypeName { get; set; }

        [XmlElement("СвФЛ")]
        public EgrIpFlInfo FlInfo { get; set; }

        [XmlElement("СвРожд")]
        public EgrIpBirthInfo BirthInfo { get; set; }

        [XmlElement("СвГражд")]
        public EgrIpCitizenshipInfo Citizenship { get; set; }

        [XmlElement("СвУдЛичнФЛ")]
        public EgrIpFlIdentityDocInfo IdentityDoc { get; set; }

        [XmlElement("СвПравЖитРФ")]
        public EgrIpRFResidenceRightInfo RFResidenceRight { get; set; }

        [XmlElement("СвАдрМЖ")]
        public EgrIpHomeAddressInfo HomeAddress { get; set; }

        [XmlElement("СвАдрЭлПочты")]
        public EgrIpEmailInfo EmailInfo { get; set; }

        [XmlElement("СвРегИП")]
        public EgrIpRegInfo IpRegInfo { get; set; }

        [XmlElement("СвРегОрг")]
        public EgrIpRegOrgInfo RegOrgInfo { get; set; }

        [XmlElement("СвСтатус")]
        public EgrIpStatusInfo StatusInfo { get; set; }

        [XmlElement("СвПрекращ")]
        public EgrIpTerminationInfo TerminationInfo { get; set; }

        [XmlElement("СвУчетНО")]
        public EgrIpTaxRegInfo TaxRegInfo { get; set; }

        [XmlElement("СвРегПФ")]
        public EgrIpPFRegInfo PFRegInfo { get; set; }

        [XmlElement("СвРегФСС")]
        public EgrIpFSSRegInfo FSSRegInfo { get; set; }

        [XmlElement("СвОКВЭД")]
        public EgrIpOkvedInfo OkvedInfo { get; set; }

        [XmlElement("СвЛицензия")]
        public List<EgrIpLicenseInfo> License { get; set; }

        [XmlElement("СвЗапЕГРИП")]
        public List<EgrIpInsertedRowsInfo> InsertedRowsInfo { get; set; }
    }
}
