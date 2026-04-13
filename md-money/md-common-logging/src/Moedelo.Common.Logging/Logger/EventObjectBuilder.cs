using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using Moedelo.Common.Logging.Extensions;
using Moedelo.Common.Logging.ExtraLog.Abstractions;

namespace Moedelo.Common.Logging.Logger
{
    internal static class EventObjectBuilder
    {
        public static IDictionary<string, object> GetEventObject(string appName, string hostName, LogMessage logMessage)
        {
            var fullMessage = GetFullMessage(logMessage.Message, logMessage.Exception);
            var logEvent = (IDictionary<string, object>) new ExpandoObject();

            logEvent.Add(LogFields.Timestamp, DateTime.Now);
            logEvent.Add(LogFields.Level, logMessage.Level.ToString());
            logEvent.Add(LogFields.AppName, appName);
            logEvent.Add(LogFields.Logger, logMessage.Logger);
            logEvent.Add(LogFields.Host, hostName);
            logEvent.Add(LogFields.Message, fullMessage);

            if (logMessage.Exception != null)
            {
                logEvent.Add(LogFields.StackTrace, logMessage.Exception.GetExceptionStackTrace());
            }

            foreach (var extraLogField in logMessage.ExtraLogFields ?? Array.Empty<ExtraLogField>())
            {
                if (logEvent.ContainsKey(extraLogField.Name))
                {
                    continue;
                }
                
                logEvent.Add(extraLogField.Name, extraLogField.IsInt ? extraLogField.IntValue : extraLogField.StringValue);
            }
            
            return logEvent;
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

            Exception currentException = exception;

            do
            {
                builder.Append($"{currentException.GetType()}: {currentException.GetInvariantCultureMessage()}");

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
    }
}