using System;
using System.Linq;
using Moedelo.Common.Audit.Configuration.Models;

namespace Moedelo.Common.Audit.Configuration.Helpers
{
    internal static class KeysHelper
    {
        private const string KeyPartCmd = "Cmd";

        private const string Prefix = "AuditTrail";

        internal static ParsedKey ParseKey(string key)
        {
            var arr = key.Split(':');
            
            if (arr.Length < 4)
            {
                return null;
            }

            if (Prefix != arr[0] || arr[1] != KeyPartCmd)
            {
                return null;
            }

            var app = arr[2];
            string cmd = arr[3];

            return new ParsedKey(app, cmd, arr.Skip(4));
        }

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
}