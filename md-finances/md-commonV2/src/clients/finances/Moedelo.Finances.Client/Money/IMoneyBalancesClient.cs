using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Finances.Client.Money.Dto;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Client.Money
{
    public interface IMoneyBalancesClient : IDI
    {
        /// <summary> Получить остатки </summary>
        Task<List<MoneySourceBalanceDto>> GetAsync(int firmId, int userId, BalanceRequestDto request);

        /// <summary> Инициировать сверку остатков с сервисом </summary>
        Task ReconcileWithServiceAsync(int firmId, int userId, ReconcileRequestDto request);
    }
}
