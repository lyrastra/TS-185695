using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Base
{
    public class EgrIpGrnIpBase
    {
        [XmlElement("ГРНИПДата")]
        public EgrIpGrnIpInfo GrnIpDate { get; set; }

        [XmlElement("ГРНИПДатаИспр")]
        public EgrIpGrnIpInfo GrnIpDateCorrection { get; set; }
    }
}
