using Moedelo.Common.Enums.Enums.ElectronicReports;

namespace Moedelo.ErptV2.Dto.ErptDocuments
{
    public class SendFileDto
    {
        public int FirmId { get; set; }
        public int UserId { get; set; }
        public string Session { get; set; }
        public FundType Fund { get; set; }
        public FundType Direction { get; set; }
        public string Department { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public string ClientIp { get; set; }

        public string Base64Content { get; set; }
        public int? EtrustId { get; set; }
        public string TrusteeInn { get; set; }
    }
}
