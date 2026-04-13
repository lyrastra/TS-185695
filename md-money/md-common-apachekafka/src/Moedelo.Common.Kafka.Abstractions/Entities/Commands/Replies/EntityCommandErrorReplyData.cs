namespace Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies
{
    public sealed class EntityCommandErrorReplyData : IEntityCommandReplyData
    {
    }

    public sealed class EntityCommandErrorReplyData<T> : IEntityCommandReplyData
    {
        static EntityCommandErrorReplyData()
        {
            EntityCommandReplyTypeMapper.AddTypeMap<EntityCommandErrorReplyData<T>>($"EntityCommandErrorReplyData_{typeof(T)}");
        }
        
        public EntityCommandErrorReplyData(T data)
        {
            Data = data;
        }

        public T Data { get; }
    }
}