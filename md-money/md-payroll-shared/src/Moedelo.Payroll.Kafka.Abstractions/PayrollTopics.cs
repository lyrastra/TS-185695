namespace Moedelo.Payroll.Kafka.Abstractions
{
    public static class PayrollTopics
    {
        public static class Events
        {
            private const string Prefix = "Payroll.Event";

            public static readonly string ChargeCudEvent = $"{Prefix}.Charge.Cud";

            public static readonly string FssRegistryDataCudEvent = $"{Prefix}.FssRegistryData.Cud";

            public static readonly string WorkerCudEvent = $"{Prefix}.Worker.Cud";

            public static readonly string PaymentEventFile =  $"{Prefix}.PaymentEventFile";
            public static readonly string PaymentEventFileEntityName =  "PaymentEventFile";

            public static readonly string FirmSalarySetting =  $"{Prefix}.FirmSalarySetting";

            public static readonly string AutoPayment =  $"{Prefix}.AutoPayment";
            public static readonly string AutoPaymentEntityName = "AutoPayment";
        }
    }
}