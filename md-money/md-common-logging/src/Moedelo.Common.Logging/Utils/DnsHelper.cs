namespace Moedelo.Common.Logging.Utils;

internal static class DnsHelper
{
    internal static string GetHostName()
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
}
