using Moedelo.Common.Enums.Enums.ElectronicReports;
using Moedelo.Common.Enums.Enums.Mime;

namespace Moedelo.ErptV2.Client.EdsDocuments
{
    public class SignatureDocumentRequest
    {
        public int FirmId { get; set; }
        public int UserId { get; set; }
        public FileTypes? Format { get; set; }
        public string Date { get; set; }
        public EdsProvider? EdsProvider { get; set; }
    }
}
