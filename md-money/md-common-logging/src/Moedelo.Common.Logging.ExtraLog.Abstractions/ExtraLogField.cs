using System;

namespace Moedelo.Common.Logging.ExtraLog.Abstractions
{
    public readonly struct ExtraLogField
    {
        private readonly Type type;

        public ExtraLogField(string name, string value)
        {
            Name = name;
            StringValue = value;
            IntValue = default;
            type = typeof(string);
        }

        public ExtraLogField(string name, int value)
        {
            Name = name;
            IntValue = value;
            StringValue = default;
            type = typeof(string);
        }

        public string Name { get; }
        public string StringValue { get; }
        public int IntValue { get; }

        public bool IsInt => type == typeof(int);
        public bool IsString => type == typeof(string);
    }
}