using Moedelo.Common.Enums.Enums.ElectronicReports;

namespace Moedelo.ErptV2.Dto.Demands
{
    public class NeformalDocumentDataDto
    {
            public int FirmId { get; set; }
            public int NeformalDocumentId { get; set; }
            public int UploadedFileId { get; set; }
            public int Knd { get; set; }
            public NeformalDocumentConditionType ConditionType { get; set; }
    }
}