using System;
using System.Collections.Concurrent;
using EasyNetQ;
using EasyNetQ.DI;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.RabbitMQ.Abstractions.Models;
using Moedelo.Infrastructure.RabbitMQ.Interfaces;

namespace Moedelo.Infrastructure.RabbitMQ.Pools
{
    [InjectAsSingleton(typeof(IAdvancedBusPool))]
    internal sealed class AdvancedBusPool : IAdvancedBusPool, IDisposable
    {
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
        
        private class DefaultAndLegacyTypeNameSerializer : ITypeNameSerializer
        {
            private readonly ITypeNameSerializer serializer = new DefaultTypeNameSerializer();
            private readonly ITypeNameSerializer serializerLegacy = new LegacyTypeNameSerializer();

            public string Serialize(Type type)
            {
                return CustomSerialize(type) ?? serializerLegacy.Serialize(type);
            }

            public Type DeSerialize(string typeName)
            {
                return CustomDesiarializer(serializerLegacy, typeName)
                       ?? CustomDesiarializer(serializer, typeName);
            }

            private static Type CustomDesiarializer(ITypeNameSerializer ser, string typeName)
            {
                try
                {
                    var type = ser.DeSerialize(typeName);
                    return type;
                }
                catch (Exception)
                {
                    return null;
                }
            }

            private static string CustomSerialize(Type type)
            {
                var genTypes = type.GetGenericArguments();
                if (genTypes.Length == 0)
                {
                    return null;
                }

                var genType = genTypes[0];
                var cutTypeAttr = (CustomRabbitMqTypeAttribute) Attribute.GetCustomAttribute(genType, typeof(CustomRabbitMqTypeAttribute));
                if (cutTypeAttr == null)
                {
                    return null;
                }

                var typeName = cutTypeAttr.TypeName;
                if (string.IsNullOrEmpty(typeName))
                {
                    return null;
                }

                return typeName;
            }
        }
    }
}