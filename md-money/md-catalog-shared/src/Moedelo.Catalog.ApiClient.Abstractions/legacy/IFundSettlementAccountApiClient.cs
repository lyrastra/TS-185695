using System.Threading.Tasks;

namespace Moedelo.Catalog.ApiClient.Abstractions.legacy
{
    /// <summary>
    /// Получение единого казначейского счета по номеру казначейского счета
    /// </summary>
    public interface IFundSettlementAccountApiClient
    {
        Task<string> MatchAsync(string settlementAccount);
    }
}
