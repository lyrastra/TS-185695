using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Consul.Abstractions;
using Moedelo.Common.Consul.ServiceDiscovery.Abstractions;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Kafka.Abstractions;
using Moedelo.Common.Kafka.Extensions;
using Moedelo.Common.Kafka.Models;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Extensions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Interfaces;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Common.Kafka;

[InjectAsSingleton(typeof(IKafkaConsumerBalancer))]
internal sealed class KafkaConsumerBalancer : IKafkaConsumerBalancer, IDisposable
{
    private readonly IKafkaConsumerStarter consumerStarter;
    private readonly IMoedeloConsulServiceDiscovery consulServiceDiscovery;
    private readonly IMoedeloConsulApiClient consulApiClient;
    private readonly EnumSettingValue<LogLevel> logLevelSetting;
    private readonly SettingValue isEnabledSetting;
    private readonly ILogger<KafkaConsumerBalancer> logger;
    private readonly ConcurrentQueue<IDisposable> consumersList = new ();
    private readonly IKafkaConsumersQuotaWatcher quotaWatcher;

    private LogLevel LogLevel => logLevelSetting.Value;

    public KafkaConsumerBalancer(
        IKafkaConsumerStarter consumerStarter,
        IMoedeloConsulServiceDiscovery consulServiceDiscovery,
        IMoedeloConsulApiClient consulApiClient,
        ISettingRepository settingRepository,
        ILogger<KafkaConsumerBalancer> logger,
        IKafkaConsumersQuotaWatcher quotaWatcher)
    {
        this.consumerStarter = consumerStarter;
        this.consulServiceDiscovery = consulServiceDiscovery;
        this.consulApiClient = consulApiClient;
        this.logger = logger;
        this.quotaWatcher = quotaWatcher;
        this.isEnabledSetting = settingRepository.Get("IsKafkaConsumersRebalanceEnabled");
        this.logLevelSetting = settingRepository.GetEnum("KafkaConsumersRebalanceLogLevel",LogLevel.Trace);
    }

    public void Dispose()
    {
        consumersList.SafeClear();
    }

    public async ValueTask StartAutoBalanceAsync<TMessage>(
        ConsumerAutoBalanceSettings<TMessage> autoBalanceSettings,
        CancellationToken cancellationToken) where TMessage : MoedeloKafkaMessageValueBase
    {
        // Дожидаемся окончания регистрации приложения в Service Discovery
        await consulServiceDiscovery.WaitForRegistrationCompleteAsync(cancellationToken).ConfigureAwait(false);

        var connectionSettings = autoBalanceSettings.Config;
        var consumerSettings = CreateKafkaConsumerSettings(autoBalanceSettings);

        if (!isEnabledSetting.GetBoolValueOrDefault(false))
        {
            logger.LogBalanceRegression(LogLevel, connectionSettings);

            // ребалансировка отключена, в этом случае стартуем одного консьюмера и ждём его завершения
            await consumerStarter
                .ListenAsync(consumerSettings, cancellationToken)
                .ConfigureAwait(false);

            return;
        }

        var consumers = new ConsumerObserverCollection<TMessage>(
            consumerSettings,
            consumerStarter.ListenAsync,
            cancellationToken);

        consumersList.Enqueue(consumers);

        var balanceContext = quotaWatcher
            .CreateServiceKafkaBalanceContext(connectionSettings, consulServiceDiscovery.ServiceId);

        // Создаем ключ в консуле, по которому будут выдаваться квоты
        // Делаем это до подписки на изменения в нём 
        await consulApiClient
            .NotifyAboutQuotaHasBeenAppliedAsync(balanceContext, consumersCount: 0, cancellationToken)
            .ConfigureAwait(false);

        logger.LogReadyToBalance(LogLevel, consumers);

        // Подписываемся на изменения в каталоге с флагами/квотами консумеров
        await quotaWatcher
            .WatchQuotaChangesAsync(balanceContext,
                quota => OnQuotaChanged(balanceContext, quota, consumers),
                cancellationToken)
            .ConfigureAwait(false);

        try
        {
            await cancellationToken;
        }
        catch(OperationCanceledException)
        {
        }
    }

    private async Task OnQuotaChanged<TMessage>(
        ServiceKafkaBalanceContext balanceContext,
        ServiceKafkaBalanceState balanceState,
        ConsumerObserverCollection<TMessage> consumers) where TMessage : MoedeloKafkaMessageValueBase
    {
        var rebalanceRequirements = balanceState.GetRebalanceRequirements(consumers.Count);

        await RebalanceAsync(consumers, balanceContext, rebalanceRequirements).ConfigureAwait(false);
    }

    private async Task RebalanceAsync<TMessage>(
        ConsumerObserverCollection<TMessage> consumers,
        ServiceKafkaBalanceContext balanceContext,
        KafkaRebalanceRequirements requirements) where TMessage : MoedeloKafkaMessageValueBase
    {
        if (requirements.IsEmpty())
        {
            // ребалансировка не нужна
            return;
        }

        var needToChangeConsumersCount = requirements.StartNewCount > 0 || requirements.StopCount > 0; 

        if (needToChangeConsumersCount)
        {
            logger.LogRebalanceStart(LogLevel, consumers, requirements);
            // замораживаем дальнейшую работу по балансировке консьюмеров на время запуска и остановки консьюмеров
            await consulApiClient
                .NotifyAboutQuotaIsBeingAppliedAsync(balanceContext, requirements, CancellationToken.None)
                .ConfigureAwait(false);

            // запускаем недостающие конcьюмеры
            StartNewConsumers(consumers, requirements.StartNewCount);
            // останавливаем лишние консьюмеры
            await StopRedundantConsumersAsync(consumers, requirements.StopCount).ConfigureAwait(false);
        }

        // уведомляем о завершении ребалансировки
        await consulApiClient
            .NotifyAboutQuotaHasBeenAppliedAsync(balanceContext, consumers.Count, CancellationToken.None)
            .ConfigureAwait(false);

        if (needToChangeConsumersCount)
        {
            logger.LogRebalanceFinish(LogLevel, consumers, requirements);
        }
    }

    private async Task StopRedundantConsumersAsync<TMessage>(
        ConsumerObserverCollection<TMessage> consumers,
        int stopCount) where TMessage : MoedeloKafkaMessageValueBase
    {
        if (stopCount == 0)
        {
            return;
        }

        // максимальное время, которое надо ждать остановки обработки сообщения
        var maxStopTimeout = TimeSpan
            .FromMilliseconds(consumers.Settings.OptionalParams.MaxPollIntervalMs ?? ReadTopicSettingDefaults.MaxPollIntervalMs);

        var stopResultList = await Task
            .WhenAll(Enumerable
                .Range(0, stopCount)
                .Select(_ => StopRedundantConsumerAsync(consumers, maxStopTimeout)))
            .ConfigureAwait(false);

        var stoppedCount = stopResultList.Count(success => success);

        logger.LogErrorIfStopMiscountPresents(consumers, stopCount, stoppedCount);
    }

    private async Task<bool> StopRedundantConsumerAsync<TMessage>(
        ConsumerObserverCollection<TMessage> consumers,
        TimeSpan maxStopTimeout) where TMessage : MoedeloKafkaMessageValueBase
    {
        if (await consumers.StopAndRemoveConsumerAsync(maxStopTimeout).ConfigureAwait(false))
        {
            logger.LogConsumerHasBeingStopped(consumers);
            return true;
        }

        // скорее всего, консьюмер остановится спустя какое-то время, но нам дождаться этого момента не удалось

        logger.LogConsumerStoppingError(consumers);
        return false;
    }

    private void StartNewConsumers<TMessage>(
        ConsumerObserverCollection<TMessage> consumers, int count)
        where TMessage : KafkaMessageValueBase
    {
        while (count-- > 0)
        {
            consumers.StartNew();

            logger.LogConsumerHasBeingStarted(consumers);
        }
    }

    private static KafkaConsumerSettings<TMessage> CreateKafkaConsumerSettings<TMessage>(
        ConsumerAutoBalanceSettings<TMessage> balanceSettings)  where TMessage : MoedeloKafkaMessageValueBase
    {
        var connectionSettings = balanceSettings.Config;
        var onMessage = balanceSettings.OnMessage;
        var messageHandlers = new KafkaConsumerHandlers<TMessage>((m, _) => onMessage(m))
            .WithFatalExceptionHandler(balanceSettings.OnException); 

        return new KafkaConsumerSettings<TMessage>(
            connectionSettings,
            messageHandlers,
            balanceSettings.ConsumerFactoryType);
    }
}
