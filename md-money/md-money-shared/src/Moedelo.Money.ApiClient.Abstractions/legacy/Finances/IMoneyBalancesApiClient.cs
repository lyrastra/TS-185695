using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.Money.ApiClient.Abstractions.legacy.Finances.Dtos;

namespace Moedelo.Money.ApiClient.Abstractions.legacy.Finances
{
    /// <summary>
    /// source: https://github.com/moedelo/md-commonV2/blob/fff3ac6b7d4c9fe49d37042744e78d66dc979d50/src/clients/finances/Moedelo.Finances.Client/Money/IMoneyBalancesClient.cs#L8
    /// </summary>
    public interface IMoneyBalancesApiClient
    {
        /// <summary>
        /// Получить остатки
        /// </summary>
        Task<MoneySourceBalanceDto[]> GetAsync(
            FirmId firmId,
            UserId userId,
            BalanceRequestDto request,
            HttpQuerySetting setting = default);
    }
}