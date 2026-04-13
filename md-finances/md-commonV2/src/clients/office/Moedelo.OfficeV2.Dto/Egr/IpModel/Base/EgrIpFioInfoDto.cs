using System.Xml.Serialization;

namespace Moedelo.OfficeV2.Dto.Egr.IpModel.Base
{
    [XmlRoot(Namespace = "", IsNullable = true)]
    public class EgrIpFioInfoDto
    {
        public string Surname { get; set; }
        
        public string Name { get; set; }
        
        public string Patronymic { get; set; }
    }
}