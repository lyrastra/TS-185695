using Newtonsoft.Json.Converters;

namespace Moedelo.Infrastructure.Json.Convertors
{
    public sealed class MdDateConverter : IsoDateTimeConverter
    {
        public MdDateConverter()
        {
            DateTimeFormat = DateFormats.MdDate;
        }
    }
}
