using Moedelo.Common.Enums.Enums.ElectronicReports;

namespace Moedelo.ErptV2.Client.EdsApi
{
    public class EdsRequest
    {
        public EdsRequestType EdsRequestType { get; set; }
        public bool TestMode { get; set; }
        public bool IsTestModeWithEdm { get; set; }
        public bool AllowEmptyEgrn { get; set; }
        public bool WithManualCheckingDocuments { get; set; }
        public bool WithAbnTypeEqualsToThree { get; set; }
        public FileInfo DocumentsArchive { get; set; }
        public FileInfo DocumentsArchiveSignature { get; set; }
    }
}
