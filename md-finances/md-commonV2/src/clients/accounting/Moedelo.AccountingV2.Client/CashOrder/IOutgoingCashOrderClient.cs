using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.CashOrder;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccountingV2.Client.CashOrder
{
    public interface IOutgoingCashOrderClient : IDI
    {
        /// <summary>
        /// Создать новое списание с типом "Возврат покупателю"
        /// </summary>
        /// <returns>DocumentBaseId созданного списания</returns>
        Task<long> CreateAsync(int firmId, int userId, NewRetailRefundOrderDto dto);

        [Obsolete("Use ICashOrderApiClient.GetByBaseIdsAsync")]
        Task<List<CashOrderBaseInfoDto>> GetBaseInfoByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);
    }
}