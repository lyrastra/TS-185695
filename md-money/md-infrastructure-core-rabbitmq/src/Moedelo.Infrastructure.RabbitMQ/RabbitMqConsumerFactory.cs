using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.Consumer;
using EasyNetQ.Topology;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Json;
using Moedelo.Infrastructure.RabbitMQ.Abstractions.Interfaces;
using Moedelo.Infrastructure.RabbitMQ.Abstractions.Models;
using Moedelo.Infrastructure.RabbitMQ.Interfaces;

namespace Moedelo.Infrastructure.RabbitMQ
{
    [InjectAsSingleton(typeof(IRabbitMqConsumerFactory))]
    internal sealed class RabbitMqConsumerFactory : IRabbitMqConsumerFactory, IDisposable
    {
        private static readonly ConsumerOptionalConfiguration defaultOptionalConfiguration =
            new ConsumerOptionalConfiguration();
        
        private readonly IAdvancedBusPool advancedBusPool;
        
        private bool disposed;
        
        private readonly object subscriptionResultLockObject = new object();

        private readonly List<ISubscriptionResult> subscriptionResultCollection = new List<ISubscriptionResult>();

        public RabbitMqConsumerFactory(IAdvancedBusPool advancedBusPool)
        {
            this.advancedBusPool = advancedBusPool;
        }

        public async Task ListenAsync<T>(
            ConsumerQueueExchangeConnection exchangeConsumerConnection, 
            Func<T, Task> onMessage,
            Func<T, Exception, Task> onException = null,
            ConsumerOptionalConfiguration optionalConfiguration = null) where T : class
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

            if (optionalConfiguration == null)
            {
                optionalConfiguration = defaultOptionalConfiguration;
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

            async Task OnMessageWrapper(byte[] messageBytes, MessageProperties messageProperties, MessageReceivedInfo messageReceivedInfo)
            {
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
            }

            var consumerCancellation = advancedBus.Consume(queue, OnMessageWrapper,
                configuration =>
                {
                    configuration.WithPriority(optionalConfiguration.Priority).WithPrefetchCount(optionalConfiguration.PrefetchCount);

                    if (optionalConfiguration.IsExclusive)
                    {
                        configuration.AsExclusive();
                    }
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
                throw new ObjectDisposedException(nameof(RabbitMqConsumerFactory));
            }
        }

        ~RabbitMqConsumerFactory()
        {
            Dispose(false);
        }
    }
}