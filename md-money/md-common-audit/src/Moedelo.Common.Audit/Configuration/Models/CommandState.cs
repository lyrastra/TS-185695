namespace Moedelo.Common.Audit.Configuration.Models
{
    internal sealed class CommandState
    {
        internal CommandState() : this(
            false,
            false,
            false,
            false,
            false)
        {
        }

        internal CommandState(
            bool enabled, 
            bool enabledApiHandler, 
            bool enabledOutgoingHttpRequest, 
            bool enabledDbQuery, 
            bool enabledInternalCode)
        {
            Enabled = enabled;
            EnabledApiHandler = enabledApiHandler;
            EnabledOutgoingHttpRequest = enabledOutgoingHttpRequest;
            EnabledDbQuery = enabledDbQuery;
            EnabledInternalCode = enabledInternalCode;
        }

        internal bool Enabled { get; }
        
        internal bool EnabledApiHandler { get; }
        
        internal bool EnabledOutgoingHttpRequest { get; }
        
        internal bool EnabledDbQuery { get; }
        
        internal bool EnabledInternalCode { get; }
    }
}