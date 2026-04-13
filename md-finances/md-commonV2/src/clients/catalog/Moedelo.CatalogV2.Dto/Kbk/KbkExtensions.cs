using System;

namespace Moedelo.CatalogV2.Dto.Kbk
{
    public static class KbkExtensions
    {
        public static bool IsActual(this KbkNumberDto kbk, DateTime date)
        {
            return (!kbk.ActualStartDate.HasValue || kbk.ActualStartDate <= date)
                   && (!kbk.ActualEndDate.HasValue || kbk.ActualEndDate >= date);
        }
    }
}