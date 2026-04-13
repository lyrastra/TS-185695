namespace Moedelo.Common.Audit.Configuration.Models
{
    internal sealed class KeyData
    {
        internal KeyData(ParsedKey key, string value)
        {
            Key = key;
            Value = value;
        }

        internal ParsedKey Key { get; }
        internal string Value { get; }
    }
}