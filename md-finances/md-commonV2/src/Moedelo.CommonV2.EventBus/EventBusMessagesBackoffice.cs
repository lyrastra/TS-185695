using System.Collections.Generic;
using Moedelo.CommonV2.EventBus.Backoffice;
using Moedelo.CommonV2.EventBus.Backoffice.PaymentImport;
using Moedelo.CommonV2.EventBus.Backoffice.Reports;
using Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;

namespace Moedelo.CommonV2.EventBus
{
    public partial class EventBusMessages
    {
        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<UserDeletedEvent> BackofficeBlUserDeleted;
        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<FirmEvent> BackofficeBlFirmMaintainedByPartner;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<SendReportSales2Event> BackofficeBlSendReportSales2;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<SendReportLeadChannels2Event> BackofficeBlSendReportLeadChannels2;
        
        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<GeneralRenewSubsReportRequestEvent> BackofficeGeneralRenewSubscriptionsReportOrder;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<SendReportNewActivityEvent> BackofficeBlSendReportNewActivity;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<SignatureReadyEvent> BackofficeSignatureReady;
        
        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<BackofficeBlUpload1CEvent> BackofficeBlUpload1C;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<Backoffice1CPaymentsUploadedEvent> Backoffice1CPaymentsUploaded;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<SendBillCommand> BackofficeSendBillCommand;

        public static readonly EventBusEventDefinition<OperatorAssignedForTrainingEvent> OperatorAssignedForTraining;

        public static readonly EventBusEventDefinition<OperatorAssignedForSupportEvent> OperatorAssignedForSupport;

        public static readonly EventBusEventDefinition<OperatorAssignmentCommand> OperatorAssignment;

        public static readonly EventBusEventDefinition<SyncOperatorByMainFirmCommand> SyncOperator;
        public static readonly EventBusEventDefinition<LinkMappedPaymentCommand> LinkMappedPayment;
    }
}