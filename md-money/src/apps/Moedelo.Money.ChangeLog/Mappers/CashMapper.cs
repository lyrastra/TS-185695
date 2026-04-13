using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;

namespace Moedelo.Money.ChangeLog.Mappers
{
    static class CashMapper
    {
        public static string MapToName(this CashDto cash, long cashId)
        {
            return cash?.Name ?? MapToTechnicalName(cashId);
        }

        private static string MapToTechnicalName(long cashId)
        {
            return $"Неизвестная касса #{cashId} (тех.идентификатор)";
        }
    }
}
