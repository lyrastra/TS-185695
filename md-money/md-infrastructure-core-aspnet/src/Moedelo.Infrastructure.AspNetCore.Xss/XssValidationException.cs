using System;

namespace Moedelo.Infrastructure.AspNetCore.Xss
{
    public class XssValidationException : Exception
    {
        public XssValidationException(string message, string suspiciousFieldPath, string suspiciousFieldValue)
            : base(message)
        {
            Suspicious = new SuspiciousField(suspiciousFieldPath, suspiciousFieldValue);
        }

        public struct SuspiciousField
        {
            public SuspiciousField(string path, string value)
            {
                Path = path;
                Value = value;
            }

            public string Path { get; }
            public string Value { get; }
        }

        public SuspiciousField Suspicious { get; }
    }
}
