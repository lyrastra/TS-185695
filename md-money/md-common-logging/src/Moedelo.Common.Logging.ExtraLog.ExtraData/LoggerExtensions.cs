using System;
using Microsoft.Extensions.Logging;

namespace Moedelo.Common.Logging.ExtraLog.ExtraData
{
    public static class LoggerExtensions
    {
        public static void LogTraceExtraData(
            this ILogger logger,
            object extraData,
            EventId eventId,
            Exception exception,
            string message,
            params object[] args)
        {
            ExtraDataContextAccessor.ExtraDataContext = extraData;
            logger.LogTrace(eventId, exception, message, args);
            ExtraDataContextAccessor.ExtraDataContext = null;
        }

        public static void LogTraceExtraData(
            this ILogger logger,
            object extraData,
            EventId eventId,
            string message,
            params object[] args)
        {
            ExtraDataContextAccessor.ExtraDataContext = extraData;
            logger.LogTrace(eventId, message, args);
            ExtraDataContextAccessor.ExtraDataContext = null;
        }

        public static void LogTraceExtraData(
            this ILogger logger,
            object extraData,
            Exception exception,
            string message,
            params object[] args)
        {
            ExtraDataContextAccessor.ExtraDataContext = extraData;
            logger.LogTrace(exception, message, args);
            ExtraDataContextAccessor.ExtraDataContext = null;
        }

        public static void LogTraceExtraData(
            this ILogger logger,
            object extraData,
            string message,
            params object[] args)
        {
            ExtraDataContextAccessor.ExtraDataContext = extraData;
            logger.LogTrace( message, args);
            ExtraDataContextAccessor.ExtraDataContext = null;
        }
        
        public static void LogDebugExtraData(
            this ILogger logger,
            object extraData,
            EventId eventId,
            Exception exception,
            string message,
            params object[] args)
        {
            ExtraDataContextAccessor.ExtraDataContext = extraData;
            logger.LogDebug(eventId, exception, message, args);
            ExtraDataContextAccessor.ExtraDataContext = null;
        }

        public static void LogDebugExtraData(
            this ILogger logger,
            object extraData,
            EventId eventId,
            string message,
            params object[] args)
        {
            ExtraDataContextAccessor.ExtraDataContext = extraData;
            logger.LogDebug(eventId, message, args);
            ExtraDataContextAccessor.ExtraDataContext = null;
        }

        public static void LogDebugExtraData(
            this ILogger logger,
            object extraData,
            Exception exception,
            string message,
            params object[] args)
        {
            ExtraDataContextAccessor.ExtraDataContext = extraData;
            logger.LogDebug(exception, message, args);
            ExtraDataContextAccessor.ExtraDataContext = null;
        }

        public static void LogDebugExtraData(
            this ILogger logger,
            object extraData,
            string message,
            params object[] args)
        {
            ExtraDataContextAccessor.ExtraDataContext = extraData;
            logger.LogDebug( message, args);
            ExtraDataContextAccessor.ExtraDataContext = null;
        }
        
        public static void LogInformationExtraData(
            this ILogger logger,
            object extraData,
            EventId eventId,
            Exception exception,
            string message,
            params object[] args)
        {
            ExtraDataContextAccessor.ExtraDataContext = extraData;
            logger.LogInformation(eventId, exception, message, args);
            ExtraDataContextAccessor.ExtraDataContext = null;
        }

        public static void LogInformationExtraData(
            this ILogger logger,
            object extraData,
            EventId eventId,
            string message,
            params object[] args)
        {
            ExtraDataContextAccessor.ExtraDataContext = extraData;
            logger.LogInformation(eventId, message, args);
            ExtraDataContextAccessor.ExtraDataContext = null;
        }

        public static void LogInformationExtraData(
            this ILogger logger,
            object extraData,
            Exception exception,
            string message,
            params object[] args)
        {
            ExtraDataContextAccessor.ExtraDataContext = extraData;
            logger.LogInformation(exception, message, args);
            ExtraDataContextAccessor.ExtraDataContext = null;
        }

        public static void LogInformationExtraData(
            this ILogger logger,
            object extraData,
            string message,
            params object[] args)
        {
            ExtraDataContextAccessor.ExtraDataContext = extraData;
            logger.LogInformation( message, args);
            ExtraDataContextAccessor.ExtraDataContext = null;
        }
        
        public static void LogWarningExtraData(
            this ILogger logger,
            object extraData,
            EventId eventId,
            Exception exception,
            string message,
            params object[] args)
        {
            ExtraDataContextAccessor.ExtraDataContext = extraData;
            logger.LogWarning(eventId, exception, message, args);
            ExtraDataContextAccessor.ExtraDataContext = null;
        }

        public static void LogWarningExtraData(
            this ILogger logger,
            object extraData,
            EventId eventId,
            string message,
            params object[] args)
        {
            ExtraDataContextAccessor.ExtraDataContext = extraData;
            logger.LogWarning(eventId, message, args);
            ExtraDataContextAccessor.ExtraDataContext = null;
        }

        public static void LogWarningExtraData(
            this ILogger logger,
            object extraData,
            Exception exception,
            string message,
            params object[] args)
        {
            ExtraDataContextAccessor.ExtraDataContext = extraData;
            logger.LogWarning(exception, message, args);
            ExtraDataContextAccessor.ExtraDataContext = null;
        }

        public static void LogWarningExtraData(
            this ILogger logger,
            object extraData,
            string message,
            params object[] args)
        {
            ExtraDataContextAccessor.ExtraDataContext = extraData;
            logger.LogWarning( message, args);
            ExtraDataContextAccessor.ExtraDataContext = null;
        }
        
        public static void LogErrorExtraData(
            this ILogger logger,
            object extraData,
            EventId eventId,
            Exception exception,
            string message,
            params object[] args)
        {
            ExtraDataContextAccessor.ExtraDataContext = extraData;
            logger.LogError(eventId, exception, message, args);
            ExtraDataContextAccessor.ExtraDataContext = null;
        }

        public static void LogErrorExtraData(
            this ILogger logger,
            object extraData,
            EventId eventId,
            string message,
            params object[] args)
        {
            ExtraDataContextAccessor.ExtraDataContext = extraData;
            logger.LogError(eventId, message, args);
            ExtraDataContextAccessor.ExtraDataContext = null;
        }

        public static void LogErrorExtraData(
            this ILogger logger,
            object extraData,
            Exception exception,
            string message,
            params object[] args)
        {
            ExtraDataContextAccessor.ExtraDataContext = extraData;
            logger.LogError(exception, message, args);
            ExtraDataContextAccessor.ExtraDataContext = null;
        }

        public static void LogErrorExtraData(
            this ILogger logger,
            object extraData,
            string message,
            params object[] args)
        {
            ExtraDataContextAccessor.ExtraDataContext = extraData;
            logger.LogError( message, args);
            ExtraDataContextAccessor.ExtraDataContext = null;
        }
        
        public static void LogCriticalExtraData(
            this ILogger logger,
            object extraData,
            EventId eventId,
            Exception exception,
            string message,
            params object[] args)
        {
            ExtraDataContextAccessor.ExtraDataContext = extraData;
            logger.LogCritical(eventId, exception, message, args);
            ExtraDataContextAccessor.ExtraDataContext = null;
        }

        public static void LogCriticalExtraData(
            this ILogger logger,
            object extraData,
            EventId eventId,
            string message,
            params object[] args)
        {
            ExtraDataContextAccessor.ExtraDataContext = extraData;
            logger.LogCritical(eventId, message, args);
            ExtraDataContextAccessor.ExtraDataContext = null;
        }

        public static void LogCriticalExtraData(
            this ILogger logger,
            object extraData,
            Exception exception,
            string message,
            params object[] args)
        {
            ExtraDataContextAccessor.ExtraDataContext = extraData;
            logger.LogCritical(exception, message, args);
            ExtraDataContextAccessor.ExtraDataContext = null;
        }

        public static void LogCriticalExtraData(
            this ILogger logger,
            object extraData,
            string message,
            params object[] args)
        {
            ExtraDataContextAccessor.ExtraDataContext = extraData;
            logger.LogCritical( message, args);
            ExtraDataContextAccessor.ExtraDataContext = null;
        }
    }
}