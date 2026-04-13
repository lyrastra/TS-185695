using Moedelo.Parsers.Klto1CParser.Exceptions;
using System;
using System.Text;

namespace Moedelo.Parsers.Klto1CParser.Extensions
{
    public static class EncodeExtensions
    {
        private const string Marker = "Кодировка";
        private const int MinSampleSize = 1000; //TS-88820

        private static readonly Encoding[] supportedEncodings = new[]
        {
            Encoding.GetEncoding(1251),
            Encoding.UTF8,
            Encoding.Default,
            Encoding.Unicode
        };

        public static string Encode(this byte[] bytes)
        {
            // проверяем кодировку на небольшом кусочке файла
            var sampleSize = Math.Min(MinSampleSize, bytes.Length);
            var sample = new byte[sampleSize];

            foreach (var encoding in supportedEncodings)
            {
                Array.Copy(bytes, sample, sampleSize);
                var result = encoding.GetString(sample);
                if (result.IndexOf(Marker, StringComparison.Ordinal) >= 0)
                {
                    return encoding.GetString(bytes);
                }
            }

            throw new UnknownEncodingException();
        }
    }
}
