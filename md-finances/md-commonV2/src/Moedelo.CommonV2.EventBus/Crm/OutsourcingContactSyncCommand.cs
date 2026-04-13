namespace Moedelo.CommonV2.EventBus.Crm
{
    public class OutsourcingContactSyncCommand
    {
        public int? OutsourcingClientId { get; set; }

        public int? FirmId { get; set; }

        public int ContactInfoId { get; set; }

        public int ContactPhoneId { get; set; }

        public string Phone { get; set; }

        public PhoneType Type { get; set; }

        public SyncOperationType OperationType { get; set; }

        public bool IsPrimary { get; set; }

        public bool IsNotCall { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public enum SyncOperationType
        {
            Create = 1,
            Update = 2,
            Delete = 3
        }

        public enum PhoneType
        {
            Phone = 0,
            Viber = 1,
            Telegram = 2,
            Whatsapp = 3
        }
    }
}