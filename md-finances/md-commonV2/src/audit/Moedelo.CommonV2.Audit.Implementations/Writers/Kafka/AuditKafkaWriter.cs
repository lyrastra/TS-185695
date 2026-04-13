using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Moedelo.CommonV2.Audit.Writers.Kafka.Extensions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.InfrastructureV2.ApacheKafka.Abstractions;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.CommonV2.Audit.Writers.Kafka;

[InjectAsSingleton(typeof(IAuditKafkaWriter))]
internal sealed class AuditKafkaWriter : IAuditKafkaWriter
{
    private const string tag = nameof(AuditKafkaWriter);

    private bool isErrorLoggedOnce = false;
    private readonly ILogger logger;
    private readonly SettingValue isEnabled;
    private readonly SettingValue topicBaseName;
    private readonly SettingValue brokerEndpoints;
    private readonly IKafkaProducer producer;
    private readonly IKafkaTopicNameResolver topicNameResolver;

    public AuditKafkaWriter(
        IKafkaProducer producer,
        ISettingRepository settingRepository,
        IKafkaTopicNameResolver topicNameResolver, ILogger logger)
    {
        this.topicNameResolver = topicNameResolver;
        this.logger = logger;
        this.producer = producer;
        isEnabled = settingRepository.Get("IsAuditKafkaStreamEnabled");
        topicBaseName = settingRepository.Get("AuditKafkaStreamTopicBaseName");
        brokerEndpoints = settingRepository.Get("AuditKafkaBrokerEndpoints");
    }

    public async Task WriteAsync(IReadOnlyCollection<IAuditSpan> rows)
    {
        if (rows?.Any() != true)
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

        var kafkaMessages = rows.Select(row => 
            new KafkaMessage<AuditSpanKafkaModel>(
                topicName,
                batchId, // используем один и тот же ключ, чтобы все сообщения попали в одну секцию топика
                row.MapToKafkaModelValue(utcNow))
        );

        try
        {
            await producer.ProduceAsync(endpoints, kafkaMessages, false).ConfigureAwait(false);
        }
        catch (Exception exception) when (isErrorLoggedOnce == false)
        {
            isErrorLoggedOnce = true;
            logger.Error(tag, "Ошибка при попытке записи данных аудита в кафку (доп. инфо логируется отдельным следующим сообщением)", exception: exception);

            var extraData = new
            {
                Total = rows.Count,
                CountByType = rows.GroupBy(row => row.Type)
                    .ToDictionary(grouping => grouping.Key, grouping => grouping.Count()),
                ProduceExceptionInfo = GetProduceExceptionInfo(exception)
            };
            
            logger.Error(tag, "Дополнительная информация по исключению", exception: exception, extraData: extraData);
        }
    }

    private static object GetProduceExceptionInfo(Exception exception)
    {
        var exceptionType = exception.GetType();

        if (exceptionType.Name != "ProduceException")
        {
            return null;
        }

        const BindingFlags publicGetterFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty;

        var deliveryResultProperty = exceptionType.GetProperty("DeliveryResult", publicGetterFlags);

        if (deliveryResultProperty == null)
        {
            return null;
        }

        var deliveryResultObject = deliveryResultProperty.GetValue(exception);

        if (deliveryResultObject == null)
        {
            return null;
        }

        var deliveryResultType = deliveryResultProperty.PropertyType;

        var messageProperty = deliveryResultType.GetProperty("Message", publicGetterFlags);

        if (messageProperty == null)
        {
            return null;
        }

        var messageObject = messageProperty.GetValue(deliveryResultObject);

        if (messageObject == null)
        {
            return null;
        }

        var messageType = messageObject.GetType();
        var messageKeyProperty = messageType.GetProperty("Key", publicGetterFlags);
        var messageValueProperty = messageType.GetProperty("Value", publicGetterFlags);

        if (messageKeyProperty == null || messageValueProperty == null)
        {
            return null;
        }

        return new
        {
            Key = messageKeyProperty.GetValue(messageObject),
            ValueSize = (messageValueProperty.GetValue(messageObject) as string)?.Length
        };
    }
}