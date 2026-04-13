using Moedelo.CommonV2.EventBus.ChatPlatform;
using Moedelo.CommonV2.EventBus.ChatPlatform.Events;
using Moedelo.CommonV2.EventBus.ChatPlatform.Gpt;
using Moedelo.CommonV2.EventBus.ChatPlatform.IqDialer;
using Moedelo.CommonV2.EventBus.ChatPlatform.Outsource;
using Moedelo.CommonV2.EventBus.ChatPlatform.RequestContext;
using Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;

namespace Moedelo.CommonV2.EventBus
{
    public partial class EventBusMessages
    {
        public static readonly EventBusEventDefinition<ChatPlatformOutputMessageEvent> ChatOutputMessage;

        public static readonly EventBusEventDefinition<ChatPlatformSendMessageEvent> ChatSendMessage;

        public static readonly EventBusEventDefinition<DistributedRequestEvent> ChatAssignedRequestPush;

        public static readonly EventBusEventDefinition<NewMessageEvent> ChatNewMessagePush;

        public static readonly EventBusEventDefinition<CrmMessagesToCaseEvent> ChatMessagesToCase;

        public static readonly EventBusEventDefinition<CrmAllMessagesToCaseEvent> ChatAllMessagesToCase;

        public static readonly EventBusEventDefinition<CrmFileToCaseEvent> ChatFileToCase;

        public static readonly EventBusEventDefinition<BindRequestToFirmEvent> BindRequestToFirmMessage;

        public static readonly EventBusEventDefinition<ChatAutoClosureRequestStatusChangedEvent> ChatAutoClosureRequestStatusChanged;

        public static readonly EventBusEventDefinition<ChatAutoClosureNewIncomingMessageEvent> ChatAutoClosureNewIncomingMessage;

        public static readonly EventBusEventDefinition<ChatAutoClosureNewOutcomingMessageEvent> ChatAutoClosureNewOutcomingMessage;

        public static readonly EventBusEventDefinition<RunDispatchCommand> ChatRunDispatchCommand;

        public static readonly EventBusEventDefinition<SendClientDispatchCommand> ChatClientDispatchCommand;

        public static readonly EventBusEventDefinition<UploadAttachmentsCommand> UploadAttachmentsCommand;

        public static readonly EventBusEventDefinition<FillBillingContextCommand> FillBillingContextCommand;
        
        public static readonly EventBusEventDefinition<IqDialerCreateCommand> IqDialerCreateCommand;
        /*---Для событийной модели-------------*/
        public static readonly EventBusEventDefinition<NewMessageEvent> NewRequestEvent;
        public static readonly EventBusEventDefinition<InputMessageEvent> InputMessageEvent;
        public static readonly EventBusEventDefinition<OutputMessageEvent> OutputMessageEvent;
        public static readonly EventBusEventDefinition<ChangedQueueEvent> ChangedQueueEvent;
        /*--- GPT. События----------*/
        public static readonly EventBusEventDefinition<GptConversationEvent> GptConversationEvent;
        /*--- GPT. События. Конец---*/
    }
}