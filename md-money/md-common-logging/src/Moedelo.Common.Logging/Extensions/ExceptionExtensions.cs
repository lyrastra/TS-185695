using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using Moedelo.Infrastructure.Json;

namespace Moedelo.Common.Logging.Extensions;

internal static class ExceptionExtensions
{
    internal static string GetInvariantCultureMessage(this Exception exception)
    {
        Debug.Assert(exception != null, "Unexpected null exception object");

        var oldCulture = Thread.CurrentThread.CurrentCulture;
        var oldUiCulture = Thread.CurrentThread.CurrentUICulture;

        try
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            return exception.Message;
        }
        finally
        {
            Thread.CurrentThread.CurrentCulture = oldCulture;
            Thread.CurrentThread.CurrentUICulture = oldUiCulture;
        }
    }

    internal static string GetExceptionStackTrace(this Exception ex)
    {
        try
        {
            var info = ExceptionLoggingInfo.Create(ex);

            return info.ToJsonString();
        }
        catch
        {
            return string.Empty;
        }
    }
}
