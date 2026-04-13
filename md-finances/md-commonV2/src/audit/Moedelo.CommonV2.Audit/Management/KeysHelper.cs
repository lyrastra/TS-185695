using System;
using System.Linq;

namespace Moedelo.CommonV2.Audit.Management;

internal static class KeysHelper
{
    private const string Prefix = "AuditTrail";
    private const string KeyPartCmd = "Cmd";

    internal static string GetCommandKey(string appName, string command)
    {
        if (string.IsNullOrWhiteSpace(appName))
        {
            throw new ArgumentNullException(nameof(appName));
        }
            
        if (string.IsNullOrWhiteSpace(command))
        {
            throw new ArgumentNullException(nameof(command));
        }

        return $"{Prefix}:{KeyPartCmd}:{FormatAppName(appName)}:{command}";
    }
        
    private static string FormatAppName(string appName)
    {
        return string.IsNullOrEmpty(appName)
            ? appName
            : string.Join("", appName.Select(c => char.IsDigit(c) || char.IsLetter(c) ? c : '_'));
    }
}