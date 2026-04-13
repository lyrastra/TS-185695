using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Web;
using Moedelo.InfrastructureV2.Domain.Extensions;
using Moedelo.InfrastructureV2.Domain.Interfaces.Context;
using Moedelo.InfrastructureV2.Domain.Interfaces.Http;
using Moedelo.InfrastructureV2.Json;

namespace Moedelo.InfrastructureV2.Logging.Helper
{
    internal static class LoggerHelper
    {
        private const string TimestampField = "Timestamp";
        private const string LevelField = "Level";
        private const string AppNameField = "appName";
        private const string LoggerField = "Logger";
        private const string HostField = "Host";
        private const string MessageField = "Message";
        private const string StackTraceField = "StackTrace";
        private const string RequestUrlField = "RequestUrl";
        private const string UserLoginField = "UserLogin";
        private const string FirmIdField = "FirmId";
        private const string UserIdField = "UserId";
        private const string ExtraDataField = "ExtraData";
        private const string CallerInfoField = "CallerInfo";
        private const string PidField = "PID";

        private const int MaxMessageLength = 65000;
        private const int TruncateMessageUpTo = 500;

        private static readonly string[] TruncatedRecordBaseFields =
        {
            TimestampField,
            LevelField,
            AppNameField,
            LoggerField,
            HostField,
            RequestUrlField,
            UserLoginField,
            FirmIdField,
            UserIdField,
            CallerInfoField
        };

        private static readonly string HostName = GetHostName();
        private static readonly string Pid = GetCurrentProcessId();

        internal static IDictionary<string, object> GetEventObject(
            string appName,
            string tag,
            LogLevel level,
            string message,
            IAuditContext context = null,
            Exception exception = null,
            object extraData = null,
            IHttpEnviroment httpEnvironment = null,
            string callerMemberName = "",
            string callerFilePath = "",
            int callerLineNumber = 0)
        {
            var fullMessage = GetFullMessage(message, exception);
            var logEvent = (IDictionary<string, object>) new ExpandoObject();

            logEvent.Add(TimestampField, DateTime.Now);
            logEvent.Add(LevelField, level.ToString());
            logEvent.Add(AppNameField, appName);
            logEvent.Add(LoggerField, tag);
            logEvent.Add(HostField, HostName);
            logEvent.Add(PidField, Pid);
            logEvent.Add(MessageField, fullMessage);

            if (exception != null)
            {
                logEvent.Add(StackTraceField, exception.GetExceptionInfo());
            }

            var requestUrl = GetRequestUrl(httpEnvironment);

            if (!string.IsNullOrEmpty(requestUrl))
            {
                logEvent.Add(RequestUrlField, requestUrl);
            }

            if (context != null)
            {
                logEvent.Add(FirmIdField, context.FirmId ?? -1);
                logEvent.Add(UserIdField, context.UserId ?? -1);
            }

            if (extraData != null)
            {
                try
                {
                    logEvent.Add(ExtraDataField, extraData.ToJsonString(new MdSerializationSettings
                    {
                        MaskPropertiesByAttribute = true,
                        MaskGenericSensitiveProperties = true
                    }));
                }
                catch
                {
                    //ignore
                }
            }

            try
            {
                logEvent.Add(CallerInfoField, new CallerInfo
                {
                    MemberName = callerMemberName,
                    FilePath = callerFilePath,
                    LineNumber = callerLineNumber,
                }.ToJsonString());
            }
            catch
            {
                //ignore
            }

            return logEvent;
        }

        internal static string TruncateEvent(
            string serializedEvent, 
            IDictionary<string, object> eventObject,
            Exception exception = null)
        {
            if (serializedEvent.Length <= MaxMessageLength)
            {
                return serializedEvent;
            }

            var truncatedEvent = (IDictionary<string, object>) new ExpandoObject();
            CopyFields(eventObject, truncatedEvent, TruncatedRecordBaseFields);

            truncatedEvent.Add(MessageField,
                $"Message has exceeded the {MaxMessageLength} character limit (truncated to {TruncateMessageUpTo}): {GetTruncatedMessage(eventObject)}");
            if (exception != null)
            {
                truncatedEvent.Add(StackTraceField, exception.GetStackTraceInfo());
            }

            return ConvertToJson(truncatedEvent);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static string ConvertToJson(IDictionary<string, object> eventObject)
        {
            return eventObject.ToJsonString();
        }

        internal static string SerializeToPlain(IDictionary<string, object> eventObject)
        {
            var builder = new StringBuilder();
            foreach (KeyValuePair<string, object> field in eventObject)
            {
                builder.AppendFormat(
                    "{0}: {1}; ",
                    field.Key,
                    field.Value.ToString().Replace(Environment.NewLine, "\\n"));
            }

            return builder.ToString();
        }

        private static string GetFullMessage(string message, Exception exception)
        {
            if (exception == null)
            {
                return message;
            }

            var builder = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(message))
            {
                builder.AppendFormat("{0} (", message);
            }

            var currentException = exception;

            do
            {
                builder.AppendFormat("{0}: {1}",
                    currentException.GetType(),
                    GetExceptionMessageInEnglish(currentException));

                if (currentException.InnerException != null)
                {
                    builder.Append(" --> ");
                }

                currentException = currentException.InnerException;
            } while (currentException != null);

            if (!string.IsNullOrWhiteSpace(message))
            {
                builder.Append(") ");
            }

            return builder.ToString();
        }

        private static string GetExceptionMessageInEnglish(Exception exception)
        {
            var oldCulture = Thread.CurrentThread.CurrentCulture;
            var oldUiCulture = Thread.CurrentThread.CurrentUICulture;

            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            var message = exception.Message;

            Thread.CurrentThread.CurrentCulture = oldCulture;
            Thread.CurrentThread.CurrentUICulture = oldUiCulture;

            return message;
        }

        private static string GetHostName()
        {
            try
            {
                return System.Net.Dns.GetHostName();
            }
            catch
            {
                return string.Empty;
            }
        }
        
        private static string GetCurrentProcessId()
        {
            try
            {
                return Process.GetCurrentProcess().Id.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        private static string GetRequestUrl(IHttpEnviroment httpEnvironment = null)
        {
            HttpRequest request;

            try
            {
                request = httpEnvironment?.CurrentContext.Request ?? HttpContext.Current?.Request;

                if (request == null)
                {
                    return string.Empty;
                }
            }
            catch
            {
                return string.Empty;
            }

            var queryString = request.QueryString.ToString();

            return !string.IsNullOrEmpty(queryString)
                ? $"{request.Path}?{queryString}"
                : request.Path;
        }

        private static void CopyFields(
            IDictionary<string, object> eventObject,
            IDictionary<string, object> truncatedEvent,
            string[] fieldNames)
        {
            foreach (var fieldName in fieldNames)
            {
                if (eventObject.ContainsKey(fieldName))
                {
                    truncatedEvent.Add(fieldName, eventObject[fieldName]);
                }
            }
        }

        private static string GetTruncatedMessage(IDictionary<string, object> eventObject)
        {
            if (!eventObject.ContainsKey(MessageField))
            {
                return string.Empty;
            }

            var fullMessage = eventObject[MessageField] as string;
            if (fullMessage == null)
            {
                return string.Empty;
            }

            if (fullMessage.Length <= TruncateMessageUpTo)
            {
                return fullMessage;
            }
            
            const int truncateLength = TruncateMessageUpTo / 2;
            return $"{fullMessage.Substring(0, truncateLength)}  ...  {fullMessage.Substring(fullMessage.Length - truncateLength)}";
        }
        
        private sealed class CallerInfo
        {
            public string MemberName { get; set; }
            
            public string FilePath { get; set; }
            
            public int LineNumber { get; set; }
        }
    }
}