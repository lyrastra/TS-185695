using System;
using Moedelo.Common.Enums.Enums.Requisites;

namespace Moedelo.Common.Enums.Extensions.Requisites;

public static class OpfExtensions
{
    /// <summary> Получить название ОПФ </summary>
    public static string GetTitle(this Opf opf)
    {
        return opf switch
        {
            Opf.Undefined => "Неопределено",
            Opf.IP => "ИП",
            Opf.OOO => "ООО",
            Opf.ZAO => "ЗАО",
            Opf.OAO => "ОАО",
            Opf.NKO => "НКО",
            Opf.AO => "АО",
            Opf.PAO => "ПАО",
            _ => throw new ArgumentOutOfRangeException(nameof(opf), opf, null),
        };
    }
}