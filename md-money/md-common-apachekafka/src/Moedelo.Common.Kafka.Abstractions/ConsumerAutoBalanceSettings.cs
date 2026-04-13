#nullable enable
using System;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Common.Kafka.Abstractions;

/// <summary>
/// Настройки автобалансировки количества консьюмеров
/// </summary>
/// <param name="Config">настройки консьюмера</param>
/// <param name="OnMessage">обработчик сообщений</param>
/// <param name="OnException">обработчик исключений</param>
/// <param name="ConsumerFactoryType">Тип фабрики консьюмеров</param>
/// <typeparam name="TMessage">тип сообщения</typeparam>
public readonly record struct ConsumerAutoBalanceSettings<TMessage>(
    KafkaConsumerConfig Config,
    Func<TMessage, Task> OnMessage,
    Func<Exception, Task> OnException,
    Type? ConsumerFactoryType) where TMessage : MoedeloKafkaMessageValueBase;

