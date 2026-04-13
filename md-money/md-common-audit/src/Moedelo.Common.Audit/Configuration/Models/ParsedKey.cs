using System.Collections.Generic;

namespace Moedelo.Common.Audit.Configuration.Models
{
    internal sealed class ParsedKey
    {
        internal ParsedKey(string appName, string command, IEnumerable<string> keyParts)
        {
            AppName = appName;
            Command = command;
            KeyParts = keyParts;
        }

        internal string AppName { get; }
        
        internal string Command { get; }
        
        internal IEnumerable<string> KeyParts { get; }
    }
}