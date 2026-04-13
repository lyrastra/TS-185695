using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies
{
    public interface IMoedeloEntityCommandReplyKafkaTopicWriter
    {
        Task<string> WriteCommandReplyDataAsync(IMoedeloEntityCommandReplyKafkaMessageDefinition replyDefinition);
        Task<string> WriteCommandReplyDataAsync(
            IMoedeloEntityCommandReplyKafkaMessageDefinition replyDefinition,
            CancellationToken cancellationToken);
        Task<string> WriteCommandReplyDataAsync(
            IMoedeloEntityCommandReplyKafkaMessageDefinition replyDefinition,
            ProducingSettings settings,
            CancellationToken cancellationToken);
    }
}