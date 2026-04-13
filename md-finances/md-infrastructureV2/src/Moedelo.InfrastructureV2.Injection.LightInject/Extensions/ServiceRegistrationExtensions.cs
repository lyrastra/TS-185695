using LightInject;

namespace Moedelo.InfrastructureV2.Injection.Lightinject.Extensions;

internal static class ServiceRegistrationExtensions
{
    internal static string ToLoggingMessage(this ServiceRegistration service)
    {
        if (service.ImplementingType != null)
        {
            return $"ServiceType: {service.ServiceType.GetTypeLoggingName()}, ImplementingType: {service.ImplementingType.GetTypeLoggingName()}, Lifetime: {service.Lifetime}";
        }
        if (service.FactoryExpression != null)
        {
            return $"ServiceType: {service.ServiceType.GetTypeLoggingName()}, FactoryExpression: {service.FactoryExpression}, Lifetime: {service.Lifetime}";
        }
        
        return $"ServiceType: {service.ServiceType.GetTypeLoggingName()}, Lifetime: {service.Lifetime}";
    }
}
