using Moedelo.Common.Enums.Enums.Consultations;

namespace Moedelo.Consultations.Dto.Message
{
    public class ConsultationMessageAttacheFileInfoDto
    {
        public int Id { get; set; }
        public int MessageId { get; set; }
        public ConsultationMessageType MessageType { get; set; }
        public string FileName { get; set; }
    }
}
