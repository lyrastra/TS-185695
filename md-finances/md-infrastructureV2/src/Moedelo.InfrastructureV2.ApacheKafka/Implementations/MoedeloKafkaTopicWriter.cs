using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.ApacheKafka.Abstractions;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;
using Moedelo.InfrastructureV2.Domain.Models.ApacheKafka;

namespace Moedelo.InfrastructureV2.ApacheKafka.Implementations;

[InjectAsSingleton(typeof(IMoedeloKafkaTopicWriter))]
internal sealed class MoedeloKafkaTopicWriter : IMoedeloKafkaTopicWriter
{
    private readonly IKafkaProducer producer;
    private readonly IAuditScopeManager auditScopeManager;
    private readonly IKafkaTopicNameResolver topicNameResolver;
    private readonly IMoedeloApacheKafkaConfig config;

    public MoedeloKafkaTopicWriter(
        IKafkaProducer producer,
        IAuditScopeManager auditScopeManager,
        IKafkaTopicNameResolver topicNameResolver,
        IMoedeloApacheKafkaConfig config)
    {
        this.producer = producer;
        this.auditScopeManager = auditScopeManager;
        this.topicNameResolver = topicNameResolver;
        this.config = config;
    }

    public Task<string> WriteAsync<T>(string topicName, string key, T value) where T : MoedeloKafkaMessageValueBase
    {
        if (string.IsNullOrWhiteSpace(topicName))
        {
            throw new ArgumentException(nameof(topicName));
        }
            
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentException(nameof(key));
        }

        if (value == null)
        {
            throw new ArgumentException(nameof(value));
        }

        if (string.IsNullOrWhiteSpace(value.Token))
        {
            throw new ArgumentException(nameof(value.Token));
        }

        value.Metadata ??= GenerateMetadata();
            
        if (value.AuditSpanContext == null)
        {
            var auditSpanContext = auditScopeManager.Current?.Span?.Context;
                
            if (auditSpanContext != null)
            {
                value.AuditSpanContext = new AuditSpanContext
                {
                    AsyncTraceId = auditSpanContext.AsyncTraceId,
                    TraceId = auditSpanContext.TraceId,
                    ParentId = auditSpanContext.ParentId,
                    CurrentId = auditSpanContext.CurrentId,
                    CurrentDepth = auditSpanContext.CurrentDepth,
                };
            }
        }

        var fullTopicName = topicNameResolver.GetTopicFullName(topicName);
        var message = new KafkaMessage<T>(fullTopicName, key, value);

        return producer.ProduceAsync(config.BrokerEndpoints, message);
    }

    public void EnsureRawProducerPoolIsHealthy()
    {
        producer.EnsureRawProducerPoolIsHealthy(config.BrokerEndpoints);
    }

    private static KafkaMessageValueMetadata GenerateMetadata()
    {
        return new KafkaMessageValueMetadata
        {
            MessageId = Guid.NewGuid(),
            MessageDate = DateTime.UtcNow,
        };
    }
}
