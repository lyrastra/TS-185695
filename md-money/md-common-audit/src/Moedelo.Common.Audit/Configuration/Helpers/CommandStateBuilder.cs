using System;
using Moedelo.Common.Audit.Configuration.Models;

namespace Moedelo.Common.Audit.Configuration.Helpers
{
    internal class CommandStateBuilder
    {
        public const string CommandEnable = "Enable";
        public const string CommandApiHandler = "ApiHandler";
        public const string CommandOutgoingHttpRequest = "OutgoingHttpRequest";
        public const string CommandDbQuery = "DbQuery";
        public const string CommandInternalCode = "InternalCode";
        
        private bool? enabled;
        private bool? enabledApiHandler;
        private bool? enabledOutgoingHttpRequest;
        private bool? enabledDbQuery;
        private bool? enabledInternalCode;

        internal CommandState Build()
        {
            return new CommandState(
                enabled ?? false, 
                enabledApiHandler ?? false, 
                enabledOutgoingHttpRequest ?? false,
                enabledDbQuery ?? false, 
                enabledInternalCode ?? false);
        }

        internal bool SetValue(string name, bool val)
        {
            switch (true)
            {
                case bool _ when name.Equals(CommandEnable, StringComparison.OrdinalIgnoreCase):
                    return Change(ref enabled, val);
                case bool _ when name.Equals(CommandApiHandler, StringComparison.OrdinalIgnoreCase):
                    return Change(ref enabledApiHandler, val);
                case bool _ when name.Equals(CommandOutgoingHttpRequest, StringComparison.OrdinalIgnoreCase):
                    return Change(ref enabledOutgoingHttpRequest, val);
                case bool _ when name.Equals(CommandDbQuery, StringComparison.OrdinalIgnoreCase):
                    return Change(ref enabledDbQuery, val);
                case bool _ when name.Equals(CommandInternalCode, StringComparison.OrdinalIgnoreCase):
                    return Change(ref enabledInternalCode, val);
                default:
                    return false;
            }
        }

        private static bool Change(ref bool? val, bool newVal)
        {
            if (val == newVal)
            {
                return false;
            }
            
            val = newVal;
            return true;
        }
    }
}