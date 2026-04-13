using Moedelo.CommonV2.EventBus.Common.MergeProductResult;
using Moedelo.CommonV2.EventBus.Docs;
using Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;

namespace Moedelo.CommonV2.EventBus
{
    public partial class EventBusMessages
    {
        public static readonly EventBusEventDefinition<IncomingUpdEvent> CreateUpd;

        public static readonly EventBusEventDefinition<IncomingUpdEvent> UpdateUpd;

        public static readonly EventBusEventDefinition<IncomingUpdEvent> DeleteUpd;

        public static readonly EventBusEventDefinition<ChangeNdsDeductionLinksEvent> ChangeNdsDeductionLinks;

        public static readonly EventBusEventDefinition<ProductMergeResultEvent> DocsBillMergeResult;
    }
}
