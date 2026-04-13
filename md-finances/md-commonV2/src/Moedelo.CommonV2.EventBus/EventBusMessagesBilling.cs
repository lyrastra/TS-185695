using Moedelo.CommonV2.EventBus.Billing;
using Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;

namespace Moedelo.CommonV2.EventBus
{
    public partial class EventBusMessages
    {
        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<ModifiedPaymentEvent> BillingBlModifiedPayment;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<PaymentChangedSuccessStateEvent> BillingBlPaymentChangedSuccessState;
        
        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<PaymentSuccessOnEvent> PaymentSuccessOn;
        
        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<PaymentHistoryCudEvent> PaymentHistoryCud;
        
        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<AffiliatedFirmAddedEvent> AffiliatedFirmAdded;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<RequestSendReceiptOnPayEvent> BillingRequestSendReceiptOnPay;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<ChangeFirstStartPaymentDateEvent> ChangeFirstStartPaymentDate;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<TransferToBackofficeBillingEvent> TransferToBackofficeBilling;

        public static readonly EventBusEventDefinition<PaymentSellersUpdatedEvent> PaymentSellersUpdated;

        public static readonly EventBusEventDefinition<PaymentPositionsUpdatedEvent> PaymentPositionsUpdated;
        public static readonly EventBusEventDefinition<TruncateBackofficeBillingTrialCommand> TruncateBackofficeBillingTrial;
    }
}