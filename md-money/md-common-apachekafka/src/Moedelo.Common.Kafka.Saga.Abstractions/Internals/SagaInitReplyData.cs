using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;

namespace Moedelo.Common.Kafka.Saga.Abstractions.Internals
{
    internal sealed class SagaInitReplyData<T> : IEntityCommandReplyData
        where T : ISagaStateData
    {
        static SagaInitReplyData()
        {
            EntityCommandReplyTypeMapper.AddTypeMap<EntityCommandSuccessReplyData<T>>($"SagaInitReplyData_{typeof(T)}");
        }
        
        public SagaInitReplyData(T data)
        {
            Data = data;
        }

        public T Data { get; }
    }
}