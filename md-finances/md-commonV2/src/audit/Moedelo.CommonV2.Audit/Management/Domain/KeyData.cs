namespace Moedelo.CommonV2.Audit.Management.Domain
{
    public class KeyData
    {
        public KeyData(ParsedKey key, string value)
        {
            Key = key;
            Value = value;
        }

        public ParsedKey Key { get; }
        public string Value { get; }
    }
}