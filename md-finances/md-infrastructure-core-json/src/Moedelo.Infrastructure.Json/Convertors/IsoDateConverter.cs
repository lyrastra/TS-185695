using Newtonsoft.Json.Converters;

namespace Moedelo.Infrastructure.Json.Convertors
{
    public sealed class IsoDateConverter : IsoDateTimeConverter
    {
        public IsoDateConverter()
        {
            DateTimeFormat = DateFormats.IsoDate;
        }
    }
}
