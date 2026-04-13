namespace Moedelo.LinkedDocuments.Kafka.Abstractions.Links
{
    public static class LinksTopics
    {
        public static class Event
        {
            private const string Prefix = "LinkedDocuments.Event";
            
            public static readonly string BillAndPaymentChangeLinks = $"{Prefix}.Links.BillAndPayment.Change";
            
            public static readonly string AccountingStatementAndPaymentChangeLinks = $"{Prefix}.Links.AccountingStatementAndPayment.Change";
            
            public static readonly string PaymentAndAdvanceStatementChangeLinks = $"{Prefix}.Links.PaymentAndAdvanceStatement.Change";

            public static readonly string ReceiptStatementAndPaymentChangeLinks = $"{Prefix}.Links.ReceiptStatementAndPayment.Change";
            
            public static readonly string PurchasesCurrencyInvoiceAndBudgetaryPaymentChangeLinks = $"{Prefix}.Links.PurchasesCurrencyInvoiceAndBudgetaryPayment.Change";
            
            public static readonly string CurrencyPaymentToSupplierAndPurchasesCurrencyInvoiceChangeLinks = $"{Prefix}.Links.CurrencyPaymentToSupplierAndPurchasesCurrencyInvoice.Change";
            
            public static readonly string AdvanceInvoiceAndPaymentDeleteLinks = $"{Prefix}.Links.AdvanceInvoiceAndPayment.Delete";
        }
    }
}