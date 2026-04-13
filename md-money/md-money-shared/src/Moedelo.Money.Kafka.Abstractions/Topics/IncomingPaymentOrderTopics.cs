namespace Moedelo.Money.Kafka.Abstractions.Topics
{
    /*
     * После запуска топика в Production не изменяйте его название
     */
    public static partial class PaymentOrderTopics
    {
        public const string Domain = "Money";
        
        public const string Subdomain = "PaymentOrders";
        
        public static class PaymentFromCustomer
        {
            public static class Event
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Event.PaymentFromCustomer";

                public static readonly string CUD = $"{prefix}.CUD";
            }
        }

        public static class LoanObtaining
        {
            public static class Event
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Event.LoanObtaining";

                public static readonly string CUD = $"{prefix}.CUD";
            }
        }

        public static class TransferFromCash
        {
            public static class Event
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Event.TransferFromCash";

                public static readonly string CUD = $"{prefix}.CUD";
            }
        }

        public static class ContributionToAuthorizedCapital
        {
            public static class Event
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Event.ContributionToAuthorizedCapital";

                public static readonly string CUD = $"{prefix}.CUD";
            }
        }

        public static class AccrualOfInterest
        {
            public static class Event
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Event.AccrualOfInterest";

                public static readonly string CUD = $"{prefix}.CUD";
            }
        }

        public static class RetailRevenue
        {
            public static class Event
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Event.RetailRevenue";

                public static readonly string CUD = $"{prefix}.CUD";
            }
        }

        public static class ContributionOfOwnFunds
        {
            public const string EntityName = "ContributionOfOwnFunds";
            
            public static class Event
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Event.ContributionOfOwnFunds";

                public static readonly string CUD = $"{prefix}.CUD";
            }
            
            public static class Command
            {
                
            }
        }

        public static class MediationFee
        {
            public static class Event
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Event.MediationFee";

                public static readonly string CUD = $"{prefix}.CUD";
            }
        }

        public static class TransferFromPurse
        {
            public static class Event
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Event.TransferFromPurse";

                public static readonly string CUD = $"{prefix}.CUD";
            }
        }

        public static class TransferFromCashCollection
        {
            public static class Event
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Event.TransferFromCashCollection";

                public static readonly string CUD = $"{prefix}.CUD";
            }
        }

        public static class FinancialAssistance
        {
            public static class Event
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Event.FinancialAssistance";

                public static readonly string CUD = $"{prefix}.CUD";
            }
        }

        public static class RefundFromAccountablePerson
        {
            public static class Event
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Event.RefundFromAccountablePerson";

                public static readonly string CUD = $"{prefix}.CUD";
            }
        }

        public static class TransferFromAccount
        {
            public static class Event
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Event.TransferFromAccount";

                public static readonly string CUD = $"{prefix}.CUD";
            }
        }

        public static class OtherIncoming
        {
            public static class Event
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Event.OtherIncoming";

                public static readonly string CUD = $"{prefix}.CUD";
            }
        }

        public static class RefundToSettlementAccount
        {
            public static class Event
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Event.RefundToSettlementAccount";

                public static readonly string CUD = $"{prefix}.CUD";
            }
        }

        public static class IncomingCurrencyPurchase
        {
            public static class Event
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Event.IncomingCurrencyPurchase";

                public static readonly string CUD = $"{prefix}.CUD";
            }

            public static class Command
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Command.IncomingCurrencyPurchase";

                public static readonly string CUD = $"{prefix}.CUD";
            }
        }

        public static class IncomingCurrencySale
        {
            public static class Event
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Event.IncomingCurrencySale";

                public static readonly string CUD = $"{prefix}.CUD";
            }

            public static class Command
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Command.IncomingCurrencySale";

                public static readonly string CUD = $"{prefix}.CUD";
            }
        }

        public static class CurrencyOtherIncoming
        {
            public static class Event
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Event.CurrencyOtherIncoming";

                public static readonly string CUD = $"{prefix}.CUD";
            }
        }

        public static class CurrencyPaymentFromCustomer
        {
            public static class Event
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Event.CurrencyPaymentFromCustomer";

                public static readonly string CUD = $"{prefix}.CUD";
            }

            public static class Command
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Command.CurrencyPaymentFromCustomer";

                public static readonly string CUD = $"{prefix}.CUD";
            }
        }

        public static class CurrencyTransferFromAccount
        {
            public static class Event
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Event.CurrencyTransferFromAccount";

                public static readonly string CUD = $"{prefix}.CUD";
            }

            public static class Command
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Command.CurrencyTransferFromAccount";

                public static readonly string CUD = $"{prefix}.CUD";
            }
        }
    }
}