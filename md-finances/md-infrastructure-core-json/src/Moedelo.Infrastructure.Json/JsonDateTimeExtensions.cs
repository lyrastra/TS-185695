using Moedelo.Infrastructure.Json.Convertors;
using Newtonsoft.Json;
using System;

namespace Moedelo.Infrastructure.Json
{
    public static class JsonDateTimeExtensions
    {
        public static DateTime ToMdDateTime(this string json)
        {
            return JsonConvert.DeserializeObject<DateTime>(json, new CustomDateTimeConverter(DateFormats.MdDate));
        }

        public static DateTime ToIsoDateTime(this string json)
        {
            return JsonConvert.DeserializeObject<DateTime>(json, new CustomDateTimeConverter(DateFormats.IsoDate));
        }
    }
}