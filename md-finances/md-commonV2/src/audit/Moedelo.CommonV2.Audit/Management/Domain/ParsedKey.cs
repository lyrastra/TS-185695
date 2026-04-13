using System.Collections.Generic;

namespace Moedelo.CommonV2.Audit.Management.Domain
{
    public class ParsedKey
    {
        public ParsedKey(string appName, string command, IEnumerable<string> keyParts)
        {
            AppName = appName;
            Command = command;
            KeyParts = keyParts;
        }

        public string AppName { get; }
        
        public string Command { get; }
        
        public IEnumerable<string> KeyParts { get; }
    }
}