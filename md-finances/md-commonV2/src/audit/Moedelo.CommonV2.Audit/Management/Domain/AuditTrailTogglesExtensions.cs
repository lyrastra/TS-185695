using System;
using System.Collections.Concurrent;

namespace Moedelo.CommonV2.Audit.Management.Domain;

internal static class AuditTrailTogglesExtensions
{
    private static readonly ConcurrentDictionary<string, Action<AuditTrailToggles, bool>> setters =
        new ConcurrentDictionary<string, Action<AuditTrailToggles, bool>>(StringComparer.CurrentCultureIgnoreCase)
        {
            [CommandKeys.CommandEnable] = static (commandState, value) =>
            {
                commandState.IsGloballyEnabled = value;
            },
            [CommandKeys.CommandApiHandler] = static (commandState, value) =>
            {
                commandState.IsApiHandlerEnabled = value;
            },
            [CommandKeys.CommandOutgoingHttpRequest] = static (commandState, value) =>
            {
                commandState.IsOutgoingHttpRequestEnabled = value;
            },
            [CommandKeys.CommandDbQuery] = static (commandState, value) =>
            {
                commandState.IsDbQueryEnabled = value;
            },
            [CommandKeys.CommandInternalCode] = static (commandState, value) =>
            {
                commandState.IsInternalCodeEnabled = value;
            }
        };
        
    internal static void SetEnabled(this AuditTrailToggles auditTrailToggles, string command, string boolAsString)
    {
        if (command == null)
        {
            // no changes
            return;
        }

        if (setters.TryGetValue(command, out var setter))
        {
            bool.TryParse(boolAsString, out var value);
            setter.Invoke(auditTrailToggles, value);
        }
    }
}