#nullable enable
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Filters;
using Moedelo.InfrastructureV2.WebApi.Filters;
using Moedelo.InfrastructureV2.WebApi.Handlers;

namespace Moedelo.InfrastructureV2.WebApi.Extensions;

public static class HttpConfigurationExtensions
{
    public static HttpConfiguration SetupMoedeloErrorLogging(this HttpConfiguration httpConfiguration)
    {
        return httpConfiguration.SetupExceptionLogger<UnhandledExceptionLogger>(replace: false);
    }

    public static HttpConfiguration SetupMoedeloExceptionLogger(this HttpConfiguration httpConfiguration)
    {
        return httpConfiguration.SetupExceptionLogger<UnhandledExceptionLogger>(replace: false);
    }

    public static HttpConfiguration SetupExceptionLogger<TLogger>(this HttpConfiguration httpConfiguration,
        bool replace = false)
        where TLogger : IExceptionLogger
    {
        // WORKAROUND для бага LightInject: GetAllInstances возвращает неожиданные результаты
        // после вызова GetInstance для конкретного типа, реализующего интерфейс.
        //
        // Проблема: Если сначала вызвать GetInstance<UnhandledExceptionLogger>(), а потом
        // GetAllInstances<IExceptionLogger>(), то LightInject "узнаёт" что UnhandledExceptionLogger
        // реализует IExceptionLogger и начинает возвращать его, хотя регистрации по интерфейсу нет.
        //
        // Это приводит к дублированию логгеров в ASP.NET Web API DefaultServices, т.к.
        // DefaultServices.GetServices() конкатенирует результат из DependencyResolver с 
        // _defaultServicesMulti.
        //
        // Решение: Вызвать GetServices ДО GetInstance — это "фиксирует" пустой результат в кэше.
        //
        // См. подробности: https://github.com/seesharper/LightInject/issues/610
        _ = httpConfiguration.DependencyResolver.GetServices(typeof(IExceptionLogger));
        
        var exceptionLogger = httpConfiguration.DependencyResolver.EnsureGetService<TLogger>();

        if (replace)
        {
            httpConfiguration.Services.Replace(typeof(IExceptionLogger), exceptionLogger);
        }
        else
        {
            httpConfiguration.Services.Add(typeof(IExceptionLogger), exceptionLogger);
        }

        return httpConfiguration;
    }

    public static HttpConfiguration SetupExceptionHandler<THandler>(this HttpConfiguration httpConfiguration)
        where THandler : IExceptionHandler
    {
        var exceptionHandler = httpConfiguration.DependencyResolver.EnsureGetService<THandler>();
        httpConfiguration.Services.Replace(typeof(IExceptionHandler), exceptionHandler);

        return httpConfiguration;
    }

    public static HttpConfiguration SetupMessageHandler<THandler>(this HttpConfiguration httpConfiguration)
        where THandler : DelegatingHandler
    {
        var handler = httpConfiguration.DependencyResolver.EnsureGetService<THandler>();
        httpConfiguration.MessageHandlers.Add(handler);
        
        return httpConfiguration;
    }

    public static HttpConfiguration SetupFilter<TFilter>(this HttpConfiguration httpConfiguration)
        where TFilter : IFilter
    {
        var filter = httpConfiguration.DependencyResolver.EnsureGetService<TFilter>();
        httpConfiguration.Filters.Add(filter);
        
        return httpConfiguration;
    }

    public static HttpConfiguration SetupEmptyPostAndPutRequestRejection(this HttpConfiguration httpConfiguration)
    {
        httpConfiguration.Filters.Add(new WebApiRejectPostAndPutRequestWithNullParameterAttribute());

        return httpConfiguration;
    }
}
