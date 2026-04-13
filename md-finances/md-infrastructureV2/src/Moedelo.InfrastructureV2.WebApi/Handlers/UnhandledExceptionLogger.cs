using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;

namespace Moedelo.InfrastructureV2.WebApi.Handlers;

[InjectAsSingleton(typeof(UnhandledExceptionLogger))]
public class UnhandledExceptionLogger : BaseMoedeloWebApiExceptionLogger
{
    public UnhandledExceptionLogger(ILogger logger) : base(logger)
    {
    }
}
