using Moedelo.Catalog.ApiClient.Abstractions.legacy.Dto;

namespace Moedelo.Money.ChangeLog.Mappers
{
    static class KbkMapper
    {
        public static string GetFullKbkName(this KbkDto kbkInfo)
        {
            if (kbkInfo == null)
            {
                return null;
            }

            return $"{kbkInfo.Description} КБК {kbkInfo.Number}";
        }
    }
}
