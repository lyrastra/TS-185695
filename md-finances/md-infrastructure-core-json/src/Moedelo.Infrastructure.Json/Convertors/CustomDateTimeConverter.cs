using Newtonsoft.Json.Converters;

namespace Moedelo.Infrastructure.Json.Convertors
{
    internal sealed class CustomDateTimeConverter : IsoDateTimeConverter
    {
        public CustomDateTimeConverter(string dateTimeFormat)
        {
            DateTimeFormat = dateTimeFormat;
        }
    }
}
