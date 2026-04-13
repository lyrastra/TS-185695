using System;
using Moedelo.CommonV2.EventBus.Crm;
using Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;

namespace Moedelo.CommonV2.EventBus
{
    public partial class EventBusMessages
    {
        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<CrmBlUserInvolvementEvent> CrmBlUserInvolvement;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<SuiteCrmBlAsteriskLeadDeleteEvent> SuiteCrmBlAsteriskLeadDelete;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<SuiteCrmBlAsteriskOpportunityDeleteEvent> SuiteCrmBlAsteriskOpportunityDelete;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<SuiteCrmBlAsteriskLeadSendEvent> SuiteCrmBlAsteriskLeadSend;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<SuiteCrmBlConvertInfoUpdateEvent> SuiteCrmBlConvertInfoUpdate;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<SuiteCrmBlLeadBankInformedUpdateEvent> SuiteCrmBlLeadBankInformedUpdate;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<SuiteCrmBlLeadLiquidityUpdateEvent> SuiteCrmBlLeadLiquidityUpdate;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<SuiteCrmBlOpportunityStatusUpdateEvent> SuiteCrmBlOpportunityStatusUpdate;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<SuiteCrmCrudTaskEvent> SuiteCrmCrudTaskCreateOrUpdate;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<SuiteCrmUpdateReservedTaskEvent> SuiteCrmReservedTaskUpdate;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<SuiteCrmCrudTaskEvent> SuiteCrmCrudTaskDelete;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<SuiteCrmCrudCallEvent> SuiteCrmCrudCallCreateOrUpdate;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<SuiteCrmUpdateReservedCallEvent> SuiteCrmReservedCallUpdate;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<SuiteCrmCrudCallEvent> SuiteCrmCrudCallDelete;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<SuiteCrmBlDocumentRecognizeRequestEvent> SuiteCrmBlDocumentRecognizeRequest;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<SuiteCrmBlDocumentEvent> SuiteCrmBlDocumentRecognizeRequest2;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<SuiteCrmBlDocumentEvent> SuiteCrmBlDocumentDownloadRequest;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<CrmBlDocumentCreatedEvent> CrmBlDocumentCreated;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<CrmBpmBlFileCreatedEvent> CrmBpmBlFileCreated;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<CrmBpmBlFileRecognizeRequestEvent> CrmBpmBlFileRecognizeRequest;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<CrmBpmBlFileRecognizeRequestEvent> CrmBpmBlFileRecognizeActRequest;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<CrmBpmBlFileRecognizedEvent> CrmBpmBlFileRecognized;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<CrmBpmBlFileDownloadRequestEvent> CrmBpmBlFileDownloadRequest;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<CrmBpmBlFileDownloadedEvent> CrmBpmBlFileDownloaded;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<CrmBpmBlFileUnarchiveEvent> CrmBpmBlFileUnarchiveRequest;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<CrmBpmBlFileUnarchiveEvent> CrmBpmBlFileUnarchived;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<CrmBpmBlFileCreatedEvent> CrmBpmBlFilePreviewCreated;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<CrmBpmBlDocumentAttachedToCaseUpdateEvent> CrmBpmBlDocumentAttachedToCaseUpdate;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<CrmBpmBlDocumentFileLinkEvent> CrmBpmBlDocumentFileLinkRequest;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<CrmBpmBlDocumentFileLinkEvent> CrmBpmBlDocumentFileLinked;

        // ReSharper disable once UnassignedReadonlyField
        [Obsolete("Use SuiteCrmThankYouBizLandingQueued or SuiteCrmThankYouBuroLandingQueued")]
        public static readonly EventBusEventDefinition<SuiteCrmThankYouLandingQueuedData> SuiteCrmThankYouLandingQueued;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<SuiteCrmThankYouLandingQueuedData> SuiteCrmThankYouBizLandingQueued;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<SuiteCrmThankYouLandingQueuedData> SuiteCrmThankYouBuroLandingQueued;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<SuiteCrmTaskFromKayakoQueuedData> SuiteCrmTaskFromKayakoQueued;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<SuiteCrmCrudTaskEvent> SuiteCrmBlTaskSendToAsterisk;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<SuiteCrmCrudTaskEvent> SuiteCrmBlTaskDeleteFromAsterisk;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<SuiteCrmBlDocumentXmlUpdatedEvent> SuiteCrmBlDocumentXmlUpdated;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<CrmChangePartnerPreferenceEvent> CrmChangePartnerPreference;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<CrmBpmBlCaseStateChangedEvent> CrmBpmBlCaseStateChanged;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<CrmOpenDlsForPartnerEvent> CrmOpenDlsForPartner;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<CrmBpmBlCaseThreadUpdatedEvent> CrmBpmBlCaseThreadUpdated;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<CrmChangePartnerStatusEvent> CrmChangePartnerStatus;

        // ReSharper disable once UnassignedReadonlyField
        /// <summary>
        /// Очередь используется только в TDC и отвечает за обновление информации отмеченных полей по лидам и сделкам
        /// </summary>
        public static readonly EventBusEventDefinition<CrmEntityFieldsUpdatedEvent> CrmEntityFieldsUpdated;

        // ReSharper disable once UnassignedReadonlyField
        /// <summary>
        /// Обновление данных для расчёта ликвидности лида
        /// </summary>
        public static readonly EventBusEventDefinition<SuiteCrmLeadLiquidityDataUpdateEvent> SuiteCrmLeadLiquidityDataUpdate;

        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<SuiteCrmLeadLoadedEvent> SuiteCrmLeadLoaded;

        // ReSharper disable once UnassignedReadonlyField
        /// <summary>
        /// Событие обновления контрагента
        /// </summary>
        public static readonly EventBusEventDefinition<SuiteCrmContractorFieldsUpdatedEvent> SuiteCrmContractorFieldsUpdated;

        // ReSharper disable once UnassignedReadonlyField
        /// <summary>
        /// Запуск upsale в АУТ
        /// </summary>
        public static readonly EventBusEventDefinition<RunUpsaleInOutsourceCommand> RunUpsaleInOutsource;

        // ReSharper disable once UnassignedReadonlyField
        /// <summary>
        /// Запуск upsale в АУТ
        /// </summary>
        public static readonly EventBusEventDefinition<RunUpsaleInOutsourceForFirmCommand> RunUpsaleInOutsourceForFirm;

        // ReSharper disable once UnassignedReadonlyField
        /// <summary>
        /// Upsale клиента в АУТ
        /// </summary>
        public static readonly EventBusEventDefinition<UpsaleClientInOutsourceCommand> UpsaleClientInOutsource;

        // ReSharper disable once UnassignedReadonlyField
        /// <summary>
        /// Создание задачи по активности пользователей для предложения оплаты
        /// </summary>
        public static readonly EventBusEventDefinition<CreateTaskForOfferPayCommand> CreateTaskForOfferPay;

        // ReSharper disable once UnassignedReadonlyField
        /// <summary>
        /// Событие по результату создания задачи по активности пользователей для предложения оплаты
        /// </summary>
        public static readonly EventBusEventDefinition<TaskForOfferPayCreationResultEvent> TaskForOfferPayCreationResult;

        // ReSharper disable once UnassignedReadonlyField
        /// <summary>
        /// Событие из CRM по созданным вручную сделкам
        /// </summary>
        public static readonly EventBusEventDefinition<SuiteCrmCrudOpportunityCreateEvent> SuiteCrmCrudOpportunityCreate;

        // ReSharper disable once UnassignedReadonlyField
        /// <summary>
        /// Создание задачи по отклику с рассылки
        /// </summary>
        public static readonly EventBusEventDefinition<CreateFunnelMailingReplyTaskCommand> CreateFunnelMailingReplyTask;

        // ReSharper disable once UnassignedReadonlyField
        /// <summary>
        /// Отправка лидов на переподписку
        /// </summary>
        public static readonly EventBusEventDefinition<LeadsOverSubscriptionCommand> LeadsOverSubscription;

        // ReSharper disable once UnassignedReadonlyField
        /// <summary>
        /// Отправка лидов на повторную обработку
        /// </summary>
        public static readonly EventBusEventDefinition<LeadsReprocessingCommand> LeadsReprocessing;
        public static readonly EventBusEventDefinition<WhatsAppLeadProcessingError> UnsuccessfulWhatsAppLeadProcessing;
        public static readonly EventBusEventDefinition<ProcessLeadThroughWhatsAppCommand> WhatsAppLeadProcessing;

        // ReSharper disable once UnassignedReadonlyField
        /// <summary>
        /// Команда на отгрузку лида в crm
        /// </summary>
        public static readonly EventBusEventDefinition<LeadLoadCommand> UploadLeadToCrm;
        
        // ReSharper disable once UnassignedReadonlyField
        /// <summary>
        /// Команда на перемещение неликвидного лида
        /// </summary>
        public static readonly EventBusEventDefinition<IlliquidLeadMovingCommand> MoveIlliquidLead;

        /// <summary>
        /// Команда на обновление данных аутсорсинга в CRM
        /// </summary>
        public static readonly EventBusEventDefinition<UpdateOutsourcingClientDataCommand> UpdateOutsourcingClientData;

        /// <summary>
        /// Команда обработки события изменения аутсорсинговой задачи по разовой услуге
        /// </summary>
        public static readonly EventBusEventDefinition<ProcessOneTimeServiceTaskUpdateCommand> ProcessOneTimeServiceTaskUpdate;

        /// <summary>
        /// Команда отправки сделки в АО
        /// </summary>
        public static readonly EventBusEventDefinition<OpportunityDialerSendingCommand> SendOpportunityToDialer;

        /// <summary>
        /// Команда отправки сделки в КЦ
        /// </summary>
        public static readonly EventBusEventDefinition<OpportunityCallCenterSendingCommand> SendOpportunityToCallCenter;

        /// <summary>
        /// Команда для обработки недозвона по сделке
        /// </summary>
        public static readonly EventBusEventDefinition<MissedCallOnOpportunityCommand> MissedCallOnOpportunity;

        /// <summary>
        /// Команда для обработки закрепления сделки за менджером
        /// </summary>
        public static readonly EventBusEventDefinition<AssignOpportunityToManagerCommand> AssignOpportunityToManager;

        /// <summary>
        /// Команда для обновления в сделке данных о звонке
        /// </summary>
        public static readonly EventBusEventDefinition<UpdateOpportunityCallDataCommand> UpdateOpportunityCallData;

        /// <summary>
        /// Команда для обновления в контрагенте данных из налоговой
        /// </summary>
        public static readonly EventBusEventDefinition<UpdateAccountTaxDataCommand> UpdateAccountTaxDataCommand;

        /// <summary>
        /// Команда для синхронизации контактов из BPM
        /// </summary>
        public static readonly EventBusEventDefinition<OutsourcingContactSyncCommand> OutsourcingContactSynchronization;

        /// <summary>
        /// Команда для переноса контактов BPM
        /// </summary>
        public static readonly EventBusEventDefinition<OutsourcingContactTransferCommand> OutsourcingContactTransfer;

        /// <summary>
        /// Команда для создания задачи по программе "Пригласи друга"
        /// </summary>
        public static readonly EventBusEventDefinition<CreateFriendInviteTaskCommand> CreateFriendInviteTask;

        /// <summary>
        /// Команда для обновления неликвидных сделок
        /// </summary>
        public static readonly EventBusEventDefinition<UpdateIlliquidOpportunityCommand> UpdateIlliquidOpportunity;

        /// <summary>
        /// Команда отправки задачи в АО с предварительной проверкой
        /// </summary>
        public static readonly EventBusEventDefinition<TaskDialerSendingWithVerifyCommand> SendTaskToDialerWithVerify;

        /// <summary>
        /// Команда удаления лида из АО
        /// </summary>
        public static readonly EventBusEventDefinition<DialerLeadDeleteCommand> DeleteLeadFromDialer;

        /// <summary>
        /// Команда обновления даты последней авторизации в мобильном приложении
        /// </summary>
        public static readonly EventBusEventDefinition<UpdateMobileAppLastLoginDateCommand> UpdateMobileAppLastLoginDateCommand;
    }
}