using Moedelo.CommonV2.Audit.Management.Domain;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;

namespace Moedelo.CommonV2.Audit.Extensions
{
    internal static class LoggerExtensions
    {
        internal static void LogStateIfChanged(
            this ILogger logger,
            string tag,
            AuditTrailToggles currentState,
            AuditTrailToggles newState)
        {
            if (!currentState.IsEqual(newState))
            {
                logger.Info(tag, "AuditTrail: обновлено состояние", extraData: new { currentState, newState });
            }
        }

        private static bool IsEqual(this AuditTrailToggles state, AuditTrailToggles other)
        {
            if (state == null)
            {
                return other == null;
            }

            if (other == null)
            {
                return true;
            }

            return state.IsGloballyEnabled == other.IsGloballyEnabled
                   && state.IsApiHandlerEnabled == other.IsApiHandlerEnabled
                   && state.IsDbQueryEnabled == other.IsDbQueryEnabled
                   && state.IsInternalCodeEnabled == other.IsInternalCodeEnabled
                   && state.IsOutgoingHttpRequestEnabled == other.IsOutgoingHttpRequestEnabled;
        }
    }
}
