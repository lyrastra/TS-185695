using Moedelo.Common.Enums.Enums.ElectronicReports;

namespace Moedelo.ErptV2.Dto.ErptDocuments
{
    public class SendingChangedDto
    {
        public int FirmId { get; }
        public string Guid { get; }
        public ErptDocumentStatus OldStatus { get; }
        public Source Source{ get; }
        public int ChangerFirmId { get; }
        public int ChangerUserId { get; }

        public SendingChangedDto(int firmId, string guid, ErptDocumentStatus oldStatus, Source source, int changerFirmId, int changerUserId)
        {
            FirmId = firmId;
            Guid = guid;
            OldStatus = oldStatus;
            Source = source;
            ChangerFirmId = changerFirmId;
            ChangerUserId = changerUserId;
        }
    }
}