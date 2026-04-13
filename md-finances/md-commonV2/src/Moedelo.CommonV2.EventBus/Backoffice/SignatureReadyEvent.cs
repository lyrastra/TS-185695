namespace Moedelo.CommonV2.EventBus.Backoffice
{
    public class SignatureReadyEvent
    {
        public int FirmId { get; set; }

        public int UserId { get; set; }

        private SignatureReadyEvent() { }

        public SignatureReadyEvent(int firmId, int userId)
        {
            FirmId = firmId;
            UserId = userId;
        }
    }
}