using System;

namespace Moedelo.Infrastructure.Json
{
    [Flags]
    public enum MdSerializerSettingsEnum
    {
        None = 0,
        CamelCaseNamingStrategy = 1,
        ToLocalDateTimeConverter = 2,
        ToIsoDateTimeConverter = 4,
        ToMdDateTimeConverter = 8,
        TypeNameHandlingAll = 16,
        ConvertTimeSpanToGoDuration = 32
    }
}