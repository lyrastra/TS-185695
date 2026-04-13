using Microsoft.Extensions.Logging;

namespace Moedelo.Money.Numeration.Api.HostedServices.LoggerExtension;

internal static partial class LoggerExtensions
{
    [LoggerMessage(EventId = 1, Level = LogLevel.Information, Message = "{typeName} стартует")]
    internal static partial void LogHostedServiceIsStarting(this ILogger logger, string typeName);

    [LoggerMessage(EventId = 2, Level = LogLevel.Information, Message = "{typeName} остановлен")]
    internal static partial void LogHostedServiceIsStopped(this ILogger logger, string typeName);
}