namespace Moedelo.Money.Kafka.Abstractions.Topics
{
    public static partial class PaymentOrderTopics
    {
        public static class RefundToCustomer
        {
            public static class Event
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Event.RefundToCustomer";

                public static readonly string CUD = $"{prefix}.CUD";
            }
        }

        public static class BankFee
        {
            public static class Event
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Event.BankFee";

                public static readonly string CUD = $"{prefix}.CUD";
            }
        }

        public static class AgencyContract
        {
            public static class Event
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Event.AgencyContract";

                public static readonly string CUD = $"{prefix}.CUD";
            }
        }

        public static class WithdrawalOfProfit
        {
            public static class Event
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Event.WithdrawalOfProfit";

                public static readonly string CUD = $"{prefix}.CUD";
            }
        }

        public static class PaymentToSupplier
        {
            public static class Event
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Event.PaymentToSupplier";

                public static readonly string CUD = $"{prefix}.CUD";
            }
        }

        public static class PaymentToAccountablePerson
        {
            public static class Event
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Event.PaymentToAccountablePerson";

                public static readonly string CUD = $"{prefix}.CUD";
            }
        }

        public static class TransferToAccount
        {
            public static class Event
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Event.TransferToAccount";

                public static readonly string CUD = $"{prefix}.CUD";
            }
        }

        public static class WithdrawalFromAccount
        {
            public static class Event
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Event.WithdrawalFromAccount";

                public static readonly string CUD = $"{prefix}.CUD";
            }
        }

        public static class LoanRepayment
        {
            public static class Event
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Event.LoanRepayment";

                public static readonly string CUD = $"{prefix}.CUD";
            }
        }

        public static class LoanIssue
        {
            public static class Event
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Event.LoanIssue";

                public static readonly string CUD = $"{prefix}.CUD";
            }
        }

        public static class LoanReturn
        {
            public static class Event
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Event.LoanReturn";

                public static readonly string CUD = $"{prefix}.CUD";
            }
        }

        public static class OtherOutgoing
        {
            public static class Event
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Event.OtherOutgoing";

                public static readonly string CUD = $"{prefix}.CUD";
            }
        }
        
        public static class Deduction
        {
            public static class Event
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Event.Deduction";

                public static readonly string CUD = $"{prefix}.CUD";
            }
        }

        public static class PaymentToNaturalPersons
        {
            public static class Event
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Event.PaymentToNaturalPersons";

                public static readonly string CUD = $"{prefix}.CUD";
            }
        }

        public static class BudgetaryPayment
        {
            public static class Event
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Event.BudgetaryPayment";

                public static readonly string CUD = $"{prefix}.CUD";
            }
        }
        
        public static class OutgoingCurrencyPurchase
        {
            public static class Event
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Event.OutgoingCurrencyPurchase";

                public static readonly string CUD = $"{prefix}.CUD";
            }

            public static class Command
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Command.OutgoingCurrencyPurchase";

                public static readonly string CUD = $"{prefix}.CUD";
            }
        }
        
        public static class OutgoingCurrencySale
        {
            public static class Event
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Event.OutgoingCurrencySale";

                public static readonly string CUD = $"{prefix}.CUD";
            }

            public static class Command
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Command.OutgoingCurrencySale";

                public static readonly string CUD = $"{prefix}.CUD";
            }
        }

        public static class CurrencyPaymentToSupplier
        {
            public static class Event
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Event.CurrencyPaymentToSupplier";
                
                public static readonly string CUD = $"{prefix}.CUD";
            }

            public static class Command
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Command.CurrencyPaymentToSupplier";

                public static readonly string CUD = $"{prefix}.CUD";
            }
        }

        public static class CurrencyBankFee
        {
            public static class Event
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Event.CurrencyBankFee";

                public static readonly string CUD = $"{prefix}.CUD";
            }

            public static class Command
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Command.CurrencyBankFee";

                public static readonly string CUD = $"{prefix}.CUD";
            }
        }
        
        public static class CurrencyOtherOutgoing
        {
            public static class Event
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Event.CurrencyOtherOutgoing";

                public static readonly string CUD = $"{prefix}.CUD";
            }
        }
        
        public static class CurrencyTransferToAccount
        {
            public static class Event
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Event.CurrencyTransferToAccount";

                public static readonly string CUD = $"{prefix}.CUD";
            }

            public static class Command
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Command.CurrencyTransferToAccount";

                public static readonly string CUD = $"{prefix}.CUD";
            }
        }

        public static class RentPayment
        {
            public static class Event
            {
                private static readonly string prefix = $"{Domain}.{Subdomain}.Event.RentPayment";

                public static readonly string CUD = $"{prefix}.CUD";
            }
        }
    }
}
