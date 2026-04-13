using System.Threading.Channels;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Consul.Abstractions;
using Moedelo.Common.Consul.ServiceDiscovery.Abstractions;
using Moedelo.Common.Kafka.Monitoring.Abstractions;
using Moedelo.Common.Kafka.Monitoring.Extensions;
using Moedelo.Common.Kafka.Monitoring.Models;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Extensions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Json;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Common.Kafka.Monitoring.Internals;

[InjectAsSingleton(typeof(IKafkaConsulApi))]
internal sealed class KafkaConsulApi : IKafkaConsulApi, IAsyncDisposable
{
    /// <summary>
    /// Можно очередь сделать подлиннее, пусть весь процесс идёт в фоне
    /// </summary>
    private const int ChannelCapacity = 1000;
    private const LogLevel DefaultLogLevel = LogLevel.Trace;
    
    private readonly EnumSettingValue<LogLevel> logLevelSetting;
    private readonly IMoedeloConsulApiClient consulApiClient;
    private readonly IMoedeloServiceDiscoverySettings serviceDiscoverySettings;
    private readonly ChannelWriter<ConsulApiCallRequest> channelWriter;
    private readonly CancellationTokenSource cancelSaveOperations = new();
    private readonly ILogger logger;
    private readonly Task channelCompletion;

    private LogLevel LogLevel => logLevelSetting.Value;

    public KafkaConsulApi(
        IMoedeloConsulApiClient consulApiClient,
        ISettingRepository settingRepository,
        ILogger<KafkaConsulApi> logger,
        IMoedeloServiceDiscoverySettings serviceDiscoverySettings)
    {
        this.consulApiClient = consulApiClient;
        this.logLevelSetting = settingRepository.GetEnum("KafkaMonitoringConsulApiLogLevel", DefaultLogLevel);
        this.logger = logger;
        this.serviceDiscoverySettings = serviceDiscoverySettings;

        var channel = Channel.CreateBounded<ConsulApiCallRequest>(new BoundedChannelOptions(capacity: ChannelCapacity)
        {
            FullMode = BoundedChannelFullMode.Wait,
            SingleReader = false,
            SingleWriter = false 
        });

        this.channelWriter = channel.Writer;
        this.channelCompletion = channel.Reader.Completion;
        Task.Run(() => ReadChannelAsync(channel.Reader, cancelSaveOperations.Token));
    }

    private async Task ReadChannelAsync(ChannelReader<ConsulApiCallRequest> channelReader, CancellationToken saveKyCancellation)
    {
        await foreach (var callRequest in channelReader.ReadAllAsync(CancellationToken.None))
        {
            try
            {
                // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
                switch (callRequest.Type)
                {
                    case ConsulApiCallRequest.RequestType.CreateKey:
                        if (saveKyCancellation.IsCancellationRequested == false)
                        {
                            await consulApiClient
                                .SaveKeyValueAsync(callRequest.KeyPath, callRequest.Value!, saveKyCancellation);
                            logger.LogSaveKey(LogLevel, callRequest.KeyPath);
                        }
                        else
                        {
                            logger.LogSaveKeyCancelled(LogLevel, callRequest.KeyPath);
                        }

                        break;
                    case ConsulApiCallRequest.RequestType.DeleteKey:
                        await consulApiClient.DeleteKeysByPrefixAsync(callRequest.KeyPath, CancellationToken.None);
                        logger.LogDeleteKey(LogLevel, callRequest.KeyPath);
                        break;
                }
            }
            catch (Exception exception)
            {
                logger.LogConsulApiCallFailed(callRequest.Type, callRequest.KeyPath, exception);
            }
        }
    }

    public async ValueTask DisposeAsync()
    {
        logger.LogKafkaConsulStops(LogLevel);
        channelWriter.TryComplete();
        cancelSaveOperations.Cancel();
        await channelCompletion;
    }

    public ValueTask NotifyAboutConsumerStartedAsync(
        string consulConsumersDirectory,
        ConsumerStartedEvent @event,
        CancellationToken cancellation)
    {
        var keyPath = serviceDiscoverySettings.BuildConsumerConsulKey(consulConsumersDirectory, @event);
        var value = StartedConsumerInfo.Create(@event.Topic);
        var jsonValue = value.ToJsonString(MdSerializerSettingsEnum.ToMdDateTimeConverter);

        return channelWriter.WriteAsync(ConsulApiCallRequest.SaveKey(keyPath, jsonValue), cancellation);
    }
    
    public ValueTask NotifyAboutConsumerStoppedAsync(
        string consulConsumersDirectory,
        ConsumerStoppedEvent @event,
        CancellationToken cancellation)
    {
        var keyPath = serviceDiscoverySettings
            .BuildConsumerConsulKey(consulConsumersDirectory, @event);
        
        return channelWriter.WriteAsync(ConsulApiCallRequest.DeleteKey(keyPath), cancellation);
    }
    
    public ValueTask NotifyAboutConsumerSetPartitionOnPauseAsync(
        string consulConsumersDirectory,
        ConsumerPartitionSetOnPauseEvent @event,
        CancellationToken cancellation)
    {
        var keyPath = serviceDiscoverySettings
            .BuildConsumerConsulKey(consulConsumersDirectory, @event);

        var value = ConsumerSetPartitionOnPauseInfo.Create(@event.Topic, @event.Partition);
        var jsonValue = value.ToJsonString(MdSerializerSettingsEnum.ToMdDateTimeConverter);
        
        return channelWriter.WriteAsync(ConsulApiCallRequest.SaveKey(keyPath, jsonValue), cancellation);
    }
}
