using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.Consumer;
using EasyNetQ.Topology;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.EventBus.Internal.Pools;
using Moedelo.InfrastructureV2.Json;

namespace Moedelo.InfrastructureV2.EventBus.Internal
{
    [InjectAsSingleton]
    public class TopicExchangeConsumerFactory : ITopicExchangeConsumerFactory, IDisposable
    {
        private readonly IAdvancedBusPool advancedBusPool;
        
        private bool disposed;
        
        private readonly object subscriptionResultLockObject = new object();

        private readonly List<ISubscriptionResult> subscriptionResultCollection = new List<ISubscriptionResult>();

        public TopicExchangeConsumerFactory(IAdvancedBusPool advancedBusPool)
        {
            this.advancedBusPool = advancedBusPool;
        }

        public async Task ListenAsync<T>(
            ExchangeConsumerConnection exchangeConsumerConnection, 
            Func<T, Task> onMessage, 
            Func<T, Exception, Task> onException = null) where T : class
        {
            CheckDisposed();

            if (exchangeConsumerConnection == null)
            {
                throw new ArgumentNullException(nameof(exchangeConsumerConnection)); 
            }

            if (onMessage == null)
            {
                throw new ArgumentNullException(nameof(onMessage)); 
            }
            
            var advancedBus = advancedBusPool.GetAdvancedBus(exchangeConsumerConnection.ConnectionString);
            var queue = await advancedBus.QueueDeclareAsync(exchangeConsumerConnection.QueueName).ConfigureAwait(false);
            var exchange = await advancedBus.ExchangeDeclareAsync(exchangeConsumerConnection.ExchangeName, ExchangeType.Topic, delayed: true).ConfigureAwait(false);

            var bindTaskList = new Task[exchangeConsumerConnection.RoutingKeyCollection.Length];

            for (var i = 0; i < exchangeConsumerConnection.RoutingKeyCollection.Length; i++)
            {
                bindTaskList[i] = advancedBus.BindAsync(exchange, queue, exchangeConsumerConnection.RoutingKeyCollection[i]);
            }

            await Task.WhenAll(bindTaskList).ConfigureAwait(false);

            Func<byte[], MessageProperties, MessageReceivedInfo, Task> onMessageWrapper =
                async (messageBytes, messageProperties, messageReceivedInfo) =>
                {
                    if (messageReceivedInfo.Redelivered && exchangeConsumerConnection.SkipRedelivered)
                    {
                        return;
                    }

                    // десериализуем вручную, чтобы обойти строгую типизацию easymq
                    var messageJson = Encoding.UTF8.GetString(messageBytes);
                    var message = messageJson.FromJsonString<T>();

                    try
                    {
                        await onMessage(message).ConfigureAwait(false);
                    }
                    catch (Exception e)
                    {
                        if (onException != null)
                        {
                            await onException(message, e).ConfigureAwait(false);
                        }

                        throw;
                    }
                };

            var consumerCancellation = advancedBus.Consume(queue,
                onMessageWrapper,
                configuration =>
                {
                    configuration.WithPriority(0).WithPrefetchCount(exchangeConsumerConnection.PrefetchCount);
                });
            var subscriptionResult = new SubscriptionResult(exchange, queue, consumerCancellation);

            lock (subscriptionResultLockObject)
            {
                subscriptionResultCollection.Add(subscriptionResult);
            }
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            
            lock (subscriptionResultLockObject)
            {
                if (disposing)
                {
                    foreach (var subscriptionResult in subscriptionResultCollection)
                    {
                        subscriptionResult?.Dispose();
                    }
                }

                disposed = true;
            }
        }

        private void CheckDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(TopicExchangeConsumerFactory));
            }
        }

        ~TopicExchangeConsumerFactory()
        {
            Dispose(false);
        }
    }
}