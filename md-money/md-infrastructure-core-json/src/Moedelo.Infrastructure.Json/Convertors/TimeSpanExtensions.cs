using System;
using System.Text;

namespace Moedelo.Infrastructure.Json.Convertors;

internal static class TimeSpanExtensions
{
    internal static string ToGoDuration(this TimeSpan ts)
    {
        if (ts.TotalMilliseconds < 1.0)
            return "0";
        if (ts.TotalSeconds < 1.0)
            return ts.TotalMilliseconds.ToString("#ms");

        var stringBuilder = new StringBuilder();
        if ((int) ts.TotalHours > 0)
            stringBuilder.Append(ts.TotalHours.ToString("#h"));
        if (ts.Minutes > 0 || stringBuilder.Length > 0)
            stringBuilder.Append(ts.ToString("%m'm'"));
        if (ts.Seconds > 0 || stringBuilder.Length > 0)
            stringBuilder.Append(ts.ToString("%s"));
        if (ts.Milliseconds > 0)
        {
            stringBuilder.Append(".");
            stringBuilder.Append(ts.ToString("fff"));
        }
        stringBuilder.Append("s");
        return stringBuilder.ToString();
    }
}
