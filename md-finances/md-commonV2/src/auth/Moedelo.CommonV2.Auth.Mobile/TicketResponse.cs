namespace Moedelo.CommonV2.Auth.Mobile
{
    public class TicketResponse
    {
        public bool IsNotAuthorized { get; set; }

        public int FirmId { get; set; }

        public int UserId { get; set; }
    }
}