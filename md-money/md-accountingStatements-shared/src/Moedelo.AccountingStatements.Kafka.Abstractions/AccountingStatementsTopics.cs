namespace Moedelo.AccountingStatements.Kafka.Abstractions
{
    public static class AccountingStatementsTopics
    {
        private const string Domain = "AccountingStatements";

        public static class Event
        {
            private static readonly string Prefix = $"{Domain}.Event";

            public static readonly string OutgoingPaymentForIncomingDocumentsCD = $"{Prefix}.OutgoingPaymentForIncomingDocuments.CD";

            public static readonly string IncomingPaymentForOutgoingDocumentsCD = $"{Prefix}.IncomingPaymentForOutgoingDocuments.CD";

            public static readonly string PaymentForDocumentCD = $"{Prefix}.PaymentForDocument.CD";

            public static readonly string AcquiringCommissionCD = $"{Prefix}.AcquiringCommission.CD";

            public static readonly string TradingFeeCD = $"{Prefix}.TradingFee.CD";
        }

        public static class SelfCostTax
        {
            public const string EntityName = nameof(SelfCostTax);

            public static class Event
            {
                public static readonly string Topic = $"{Domain}.Event.{EntityName}";
            }
        }
    }
}