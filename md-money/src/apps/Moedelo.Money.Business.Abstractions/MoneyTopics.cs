namespace Moedelo.Money.Business.Abstractions
{
    public static class MoneyTopics
    {
        private const string Domain = "Money";

        public static class Commands
        {
            private const string CommandsPrefix = "Command";

            public static readonly string ChangeTaxationSystemCommand = $"{Domain}.{CommandsPrefix}.ChangeTaxationSystem";
            public static readonly string PaymentOrderChangeTaxationSystemCommand = $"{Domain}.PaymentOrders.{CommandsPrefix}.ChangeTaxationSystem";
            public static readonly string PaymentOrderSetMissingEmployeeCommand = $"{Domain}.PaymentOrders.{CommandsPrefix}.SetMissingEmployee";
            public static readonly string CashOrderChangeTaxationSystemCommand = $"{Domain}.CashOrders.{CommandsPrefix}.ChangeTaxationSystem";
            public static readonly string PurseOperationChangeTaxationSystemCommand = $"{Domain}.PurseOperations.{CommandsPrefix}.ChangeTaxationSystem";
        }

        public static class Events
        {
            private const string EventsPrefix = "Event";

            public static readonly string TaxationSystemChangedEvent = $"{Domain}.{EventsPrefix}.TaxationSystemChanged";
        }
    }
}
