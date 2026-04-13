using System.Diagnostics;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Entities.Messages;
using Moedelo.Infrastructure.Kafka.ErrorTolerance;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Abstractions;

namespace Moedelo.Common.Kafka.ErrorTolerance;

public static class MoedeloEntityCommandKafkaTopicReaderBuilderExtensions
{
    /// <summary>
    /// Продолжить обработку сообщений из секции после постановки её на паузу.
    /// После постановки секции топика на паузу обработка событий из неё может быть продолжена.
    /// При такой обработке:
    /// - смещение в секции двигаться не будет
    /// - будут пропускаться сообщения с тем же ключом, как у сообщения, на котором секция встала на паузу
    /// - информация об обработанных и пропущенных событиях будет сохранения в БД, что предотвратит повторную
    /// обработку и потерю пропущенных сообщений
    /// - при перезапуске консьюмера секция топика начнёт обрабатываться со смещения, на котором она была поставлена на паузу  
    /// </summary>
    public static IMoedeloEntityMessageKafkaTopicReaderBuilder<TMessageValue, TBaseReaderBuilder>
        WithContinueAfterPause<TMessageValue, TBaseReaderBuilder>(
            this IMoedeloEntityMessageKafkaTopicReaderBuilder<TMessageValue, TBaseReaderBuilder> readerBuilder,
            Action<ConsumingErrorToleranceOptions> configureOptions) where TMessageValue : MoedeloKafkaMessageValueBase
    {
        Debug.Assert(configureOptions != null);

        var options = new ConsumingErrorToleranceOptions();
        configureOptions.Invoke(options);

        if (options.KafkaConsumerMessageMemoryRepositoryType == null)
        {
            throw new ArgumentNullException(
                nameof(options.KafkaConsumerMessageMemoryRepositoryType),
                $"Поле {nameof(options.KafkaConsumerMessageMemoryRepositoryType)} должно быть выставлено");
        }

        readerBuilder.AddSettingInjector(settings =>
        {
            if (settings.AutoConsumersCount != true)
            {
                throw new Exception(
                    $"Опция {nameof(WithContinueAfterPause)} доcтупна только при ${nameof(IMoedeloEntityMessageKafkaTopicReaderBuilder<TMessageValue, TBaseReaderBuilder>.WithAutoConsumersCount)} = true");
            }

            settings.ConsumerFactoryType = typeof(IErrorToleratedKafkaConsumerFactory);
            settings.ExtraOptions.Add(options);
        });

        return readerBuilder;
    }
}
