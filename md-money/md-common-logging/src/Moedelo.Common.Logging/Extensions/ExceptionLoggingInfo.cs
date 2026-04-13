using System;
using System.Diagnostics;
// ReSharper disable MemberCanBePrivate.Global

namespace Moedelo.Common.Logging.Extensions;

internal sealed class ExceptionLoggingInfo
{
    internal static ExceptionLoggingInfo Create(Exception exception) =>
        new(exception.Message, exception.ToStringDemystified());

    private ExceptionLoggingInfo(string message, string demystifiedStacktrace)
    {
        Message = message;
        DemystifiedStacktrace = demystifiedStacktrace;
    }

    public string Message { get; }
    public string DemystifiedStacktrace { get; }
}
