namespace Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies
{
    public sealed class EntityCommandSuccessReplyData : IEntityCommandReplyData
    {
    }

    public sealed class EntityCommandSuccessReplyData<T> : IEntityCommandReplyData
    {
        static EntityCommandSuccessReplyData()
        {
            EntityCommandReplyTypeMapper.AddTypeMap<EntityCommandSuccessReplyData<T>>($"EntityCommandSuccessReplyData_{typeof(T)}");
        }
        
        public EntityCommandSuccessReplyData(T data)
        {
            Data = data;
        }

        public T Data { get; }
    }
}