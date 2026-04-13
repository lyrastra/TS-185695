using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependencyInjection;
using System.Security.Cryptography;
using System.Text;

namespace Moedelo.InfrastructureV2.Injection.Web;

[InjectAsSingleton(typeof(IServiceDiCheckerFactory))]
internal sealed class ServiceDiCheckerFactory : IServiceDiCheckerFactory
{
    private readonly IDIChecks diChecks;
    private readonly ConcurrentDictionary<string, IServiceDiChecker> checkers =
        new ConcurrentDictionary<string, IServiceDiChecker>();

    public ServiceDiCheckerFactory(IDIChecks diChecks)
    {
        this.diChecks = diChecks;
    }

    public IServiceDiChecker CreateChecker(params Type[] serviceTypes)
    {
        var hash = CalculateHash(serviceTypes);

        return checkers.GetOrAdd(hash, _ => new ServiceDiChecker(diChecks, serviceTypes));
    }

    private static string CalculateHash(IEnumerable<Type> serviceTypes)
    {
        using var md5 = MD5.Create();

        md5.Initialize();
        var combinedName = string.Join("|", serviceTypes.Select(type => type.FullName));
        var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(combinedName));

        return BitConverter.ToString(bytes).Replace("-", string.Empty).ToLower();
    }
}