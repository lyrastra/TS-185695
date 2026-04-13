using System;
using System.Web.Http.ExceptionHandling;
using Moedelo.InfrastructureV2.Domain.Interfaces.Http;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;

namespace Moedelo.InfrastructureV2.WebApi.Handlers;

public abstract class BaseMoedeloWebApiExceptionLogger : ExceptionLogger
{
    protected string Tag { get; }
    protected ILogger Logger { get; }

    protected BaseMoedeloWebApiExceptionLogger(ILogger logger)
    {
        this.Logger = logger;
        this.Tag = GetType().Name;
    }
    
    public override void Log(ExceptionLoggerContext context)
    {
        if (context.Exception == null) return;

        var httpEnvironment = GetHttpEnvironment(context);

        LogError(context.Exception, httpEnvironment);
    }

    private static IHttpEnviroment GetHttpEnvironment(ExceptionLoggerContext context)
    {
        try
        {
            return context.RequestContext.Configuration.DependencyResolver.GetService(typeof(IHttpEnviroment))
                as IHttpEnviroment;
        }
        catch
        {
            return null;
        }
    }

    private void LogError(Exception exception, IHttpEnviroment httpEnvironment)
    {
        while (exception != null)
        {
            if (exception is OperationCanceledException)
            {
                Logger.Info(Tag, $"Request was cancelled, {exception.GetType().Name}: {exception.Message}", environment: httpEnvironment);
            }
            else
            {
                Logger.Error(Tag, exception.Message, exception, environment: httpEnvironment);
            }
            exception = exception.InnerException;
        }
    }
}
