namespace Moedelo.Docs.Kafka.Abstractions.Topics
{
    /*
     * Типики для приложений на старой архитектуре
     * После запуска топика в Production не изменяйте его название
     */
    public static class AccountingPrimaryDocumentsTopics
    {
        public static string Domain = "Accounting.PrimaryDocuments";

        public static class Sales
        {
            public static class Bills
            {
                public const string CUD = "Accounting.PrimaryDocuments.Sales.Bills.Event.CUD";
                public const string PaymentChanged = "Accounting.PrimaryDocuments.Sales.Bills.Event.PaymentChanged";
            }
            public static class RetailReports
            {
                public const string CUD = "Accounting.PrimaryDocuments.Sales.RetailReports.Event.CUD";
            }
            public static class Statements
            {
                public const string CUD = "Accounting.PrimaryDocuments.Sales.Statements.Event.CUD";
            }
            
            public static class Ukds
            {
                public const string CUD = "Docs.Sales.Ukds.Event.CUD";
                public const string UpdateRefundPaymentCommand = "Docs.Sales.Ukds.Command.UpdateRefundPayment";
            }
            public static class MiddlemanReports
            {
                public const string CUD = "Accounting.PrimaryDocuments.Sales.MiddlemanReports.Event.CUD";
            }
        }

        public static class Purchases
        {
            public static string Subdomain = "Purchases";

            public static class Statements
            {
                public const string CUD = "Accounting.PrimaryDocuments.Purchases.Statements.Event.CUD";
            }

            public static class AdvanceStatements
            {
                public static string CUD = "Accounting.PrimaryDocuments.Purchases.AdvanceStatements.Event.CUD";
                public static string EntityName = "AdvanceStatement";
            }
        }
    }
}