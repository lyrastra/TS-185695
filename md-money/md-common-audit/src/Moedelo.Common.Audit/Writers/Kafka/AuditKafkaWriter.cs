using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Audit.Writers.Kafka.Extensions;
using Moedelo.Common.Kafka.Abstractions;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Interfaces;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Common.Audit.Writers.Kafka;

[InjectAsSingleton(typeof(IAuditKafkaWriter))]
internal sealed class AuditKafkaWriter : IAuditKafkaWriter
{
    private readonly SettingValue isEnabled;
    private readonly SettingValue topicBaseName;
    private readonly SettingValue brokerEndpoints;
    private readonly IKafkaProducer producer;
    private readonly IKafkaTopicNameResolver topicNameResolver;

    public AuditKafkaWriter(
        IKafkaProducer producer,
        ISettingRepository settingRepository,
        IKafkaTopicNameResolver topicNameResolver)
    {
        this.topicNameResolver = topicNameResolver;
        this.producer = producer;
        isEnabled = settingRepository.Get("IsAuditKafkaStreamEnabled");
        topicBaseName = settingRepository.Get("AuditKafkaStreamTopicBaseName");
        brokerEndpoints = settingRepository.Get("AuditKafkaBrokerEndpoints");
    }

    public async Task WriteAsync(IReadOnlyCollection<IAuditSpanData> rows)
    {
        if (rows.Count == 0)
        {
            return;
        }

        if (!isEnabled.GetBoolValueOrDefault(false))
        {
            return;
        }

        var topicName = topicNameResolver.GetTopicFullName(topicBaseName.Value);
        var endpoints = brokerEndpoints.Value;
        var utcNow = DateTime.UtcNow;
        var batchId = Guid.NewGuid().ToString();

        var kfMessages = rows.Select(row => new KafkaMessage<AuditSpanKafkaModel>(
            topicName,
            batchId, // используем один и тот же ключ, чтобы все сообщения попали в одну секцию топика
            row.MapToKafkaMessageValue(utcNow)));

        await producer.ProduceAsync(endpoints, kfMessages, false).ConfigureAwait(false);
    }
}