namespace Moedelo.CommonV2.EventBus.Crm
{
    public class CreateFriendInviteTaskCommand
    {
        public int InvitedUserId { get; set; }

        public int InvitedUserRegistrationHistoryId { get; set; }

        public string InvitedUserPhone { get; set; }

        public int InviterUserId { get; set; }

        public PaymentProductCode InviterUserPaymentProductCode { get; set; }

        public enum PaymentProductCode
        {
            IB,
            OUT
        }
    }
}