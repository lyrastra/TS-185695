using System;
using System.Collections.Generic;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Web;

namespace Moedelo.InfrastructureV2.Injection.Internals;

[InjectAsSingleton(typeof(IServicesCreationCheck), typeof(IWebAppConfigCheck))]
// ReSharper disable once ClassNeverInstantiated.Global
internal sealed class ServicesCreationCheck : IServicesCreationCheck
{
    private bool isChecked;
    private readonly object lockObject = new();
    private readonly IDIChecks idContainer;
    
    private static readonly HashSet<Type> TypesToCheck = new();
    private static bool isRegistrationClosed;

    internal static void AddTypeToCheck<TService>()
    {
        if (isRegistrationClosed)
        {
            throw new InvalidOperationException("Регистрация типов допустима только при инициализации контейнера DI");
        }

        TypesToCheck.Add(typeof(TService));
    }

    public ServicesCreationCheck(IDIChecks idContainer)
    {
        this.idContainer = idContainer;
        isRegistrationClosed = true;
    }

    public void Check()
    {
        if (isChecked) return;

        lock (lockObject)
        {
            if (isChecked) return;

            idContainer.EnsureServicesCanBeCreated(TypesToCheck);

            isChecked = true;
        }
    }
}
