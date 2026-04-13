namespace Moedelo.Billing.Kafka.Abstractions
{
    public static class BillingTopics
    {
        private const string DomainName = "Billing";

        public static class Event
        {
            //todo ввести поддомен Payments
            public static class PaymentHistoryCud
            {
                public const string EntityName = nameof(PaymentHistoryCud);
                public const string EventTopic = $"{DomainName}.Event.Payments";
            }
            
            public static class PaymentHistoryTransferred
            {
                public const string EntityName = nameof(PaymentHistoryTransferred);
                public const string EventTopic = $"{DomainName}.Event.Payments.Transfer";
            }

            public static class PaymentChanged
            {
                public const string EntityName = nameof(PaymentChanged);
                public const string EventTopic = $"{DomainName}.Event.Payments.Change";
            }
        }

        public static class PaymentTransactions
        {
            private const string SubDomain = "PaymentTransactions";

            public static class PaymentLinkedTransactions
            {
                public const string EntityName = nameof(PaymentLinkedTransactions);

                public static class Event
                {
                    //todo добавить префикс домена
                    public const string Topic = $"{SubDomain}.Event.{EntityName}";
                }
            }
        }

        public static class LimitExcess
        {
            private const string SubDomain = nameof(LimitExcess);

            public static class FirmLimitExcess
            {
                public const string EntityName = nameof(FirmLimitExcess);
                public const string CommandTopic = $"{DomainName}.{SubDomain}.Command.{EntityName}";
                public const string EventTopic = $"{DomainName}.{SubDomain}.Event.{EntityName}";
            }
        }

        public static class Bills
        {
            private const string SubDomain = nameof(Bills);

            public static class Bill
            {
                public const string EntityName = nameof(Bill);
                public const string CommandTopic = $"{DomainName}.{SubDomain}.Command.{EntityName}";
                public const string EventTopic = $"{DomainName}.{SubDomain}.Event.{EntityName}";
            }
            
            public static class Notification
            {
                public const string EntityName = nameof(Notification);
                public const string CommandTopic = $"{DomainName}.{SubDomain}.Command.{EntityName}";
            }
        }

        public static class Initiate
        {
            public const string EntityName = nameof(Initiate);
            private const string SubDomain = "AutoBilling";

            public static class Command
            {
                public const string Topic = $"{DomainName}.{SubDomain}.Command.{EntityName}";
            }

            public static class Event
            {
                public const string Topic = $"{DomainName}.{SubDomain}.Event.{EntityName}";
            }
        }

        public static class Request
        {
            public const string EntityName = nameof(Request);
            private const string SubDomain = "AutoBilling";

            public static class Command
            {
                public const string Topic = $"{DomainName}.{SubDomain}.Command.{EntityName}";
            }

            public static class Event
            {
                public const string Topic = $"{DomainName}.{SubDomain}.Event.{EntityName}";
            }
        }

        public static class BillManagement
        {
            private const string SubDomain = nameof(BillManagement);

            public static class BillChanging
            {
                public const string EntityName = nameof(BillChanging);

                public static class Command
                {
                    public const string Topic = $"{DomainName}.{SubDomain}.Command.{EntityName}";
                }

                public static class Event
                {
                    public const string Topic = $"{DomainName}.{SubDomain}.Event.{EntityName}";
                }
            }
        }

        public static class Receipts
        {
            private const string SubDomain = nameof(Receipts);

            public static class Receipt
            {
                public const string EntityName = nameof(Receipt);

                public static class Command
                {
                    public const string Topic = $"{DomainName}.{SubDomain}.Command.{EntityName}";
                }
            }
        }
        
        public static class PaymentHistory
        {
            private const string SubDomain = nameof(PaymentHistory);

            public static class PaymentCategory
            {
                public const string EntityName = nameof(PaymentCategory);

                public static class Event
                {
                    public const string EventTopic = $"{DomainName}.{SubDomain}.Event.{EntityName}";
                }
            }
        }
        
        public static class Marketplace
        {
            private const string SubDomain = nameof(Marketplace);

            public static class ProlongationAttempt
            {
                public const string EntityName = nameof(ProlongationAttempt);

                public static class Event
                {
                    public const string Topic = $"{DomainName}.{SubDomain}.Event.{EntityName}";
                }
            }
        }
    }
}