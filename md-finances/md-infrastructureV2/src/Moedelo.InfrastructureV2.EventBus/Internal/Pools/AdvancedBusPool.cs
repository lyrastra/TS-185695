using System;
using System.Collections.Concurrent;
using EasyNetQ;
using EasyNetQ.DI;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using RabbitMQ.Client;

namespace Moedelo.InfrastructureV2.EventBus.Internal.Pools
{
    [InjectAsSingleton]
    public sealed class AdvancedBusPool : IAdvancedBusPool, IDisposable
    {
#pragma warning disable 169
#pragma warning disable 414
        //НЕ УДАЛЯТЬ!!! Нужно для копирования клиента rabbitmq по зависимостям.
        private readonly ConnectionFactory factory = null;
#pragma warning restore 414
#pragma warning restore 169
        
        private ConcurrentDictionary<string, IAdvancedBus> pool = 
            new ConcurrentDictionary<string, IAdvancedBus>();
        
        private bool disposed;
        
        public IAdvancedBus GetAdvancedBus(string connectionString)
        {
            CheckDisposed();
            
            var advancedBus = pool.GetOrAdd(connectionString, BusFactory);

            return advancedBus;
        }
        
        private static IAdvancedBus BusFactory(string connectionString)
        {
            var advancedBus = RabbitHutch.CreateBus(connectionString, RegisterBusServices)
                .Advanced;

            return advancedBus;
        }
        
        private static void RegisterBusServices(IServiceRegister serviceRegister)
        {
            serviceRegister.Register<ITypeNameSerializer>(new DefaultAndLegacyTypeNameSerializer());
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

            if (disposing)
            {
                foreach (var advancedBus in pool.Values)
                {
                    advancedBus?.Dispose();
                }

                pool = null;
            }

            disposed = true;
        }

        private void CheckDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(AdvancedBusPool));
            }
        }
        
        ~AdvancedBusPool()
        {
            Dispose(false);
        }
    }
}