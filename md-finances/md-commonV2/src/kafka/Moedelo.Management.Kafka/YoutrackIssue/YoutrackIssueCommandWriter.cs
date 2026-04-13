using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;
using Moedelo.Management.Kafka.YoutrackIssue.Models;

namespace Moedelo.Management.Kafka.YoutrackIssue
{
    [InjectAsSingleton]
    public class YoutrackIssueCommandWriter : IYoutrackIssueCommandWriter
    {
        private const string EntityName = YoutrackIssueConstants.EntityName;
        private static readonly string Topic = YoutrackIssueConstants.Event.Topic;
        
        private readonly IMoedeloEntityCommandKafkaTopicWriter writer;

        public YoutrackIssueCommandWriter(
            IMoedeloEntityCommandKafkaTopicWriter writer)
        {
            this.writer = writer;
        }
        
        public Task CreateIssueAsync(string key, string token, CreateIssueCommandData commandData)
        {
            return writer.WriteCommandDataAsync(Topic, key, EntityName, commandData, token);
        }
    }
}