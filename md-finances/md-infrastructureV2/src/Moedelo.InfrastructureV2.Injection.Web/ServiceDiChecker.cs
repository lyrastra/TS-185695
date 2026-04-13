using System;
using System.Collections.Generic;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependencyInjection;

namespace Moedelo.InfrastructureV2.Injection.Web
{
    internal sealed class ServiceDiChecker : IServiceDiChecker
    {
        private readonly IReadOnlyCollection<Type> serviceTypes;
        private readonly IDIChecks diChecks;
        private volatile bool isCheckComplete = false;

        internal ServiceDiChecker(IDIChecks diChecks, IReadOnlyCollection<Type> serviceTypes)
        {
            this.serviceTypes = serviceTypes;
            this.diChecks = diChecks;
        }

        public void EnsureServicesCanBeCreated()
        {
            if (!isCheckComplete)
            {
                lock (diChecks)
                {
                    if (!isCheckComplete)
                    {
                        diChecks.EnsureServicesCanBeCreated(serviceTypes);
                        isCheckComplete = true;
                    }
                }
            }
        }
    }
}
