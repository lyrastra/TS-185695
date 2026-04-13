using Moedelo.Money.CashOrders.Domain.Models;
using Moedelo.Money.Common.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.CashOrders.DataAccess.Abstractions
{
    public interface ICashOrderDao
    {
        Task<CashOrder> GetAsync(int firmId, long documentBaseId);

        Task<long> CreateAsync(CashOrder operation);

        Task UpdateAsync(CashOrder operation);

        Task DeleteAsync(int firmId, long documentBaseId);

        Task<IReadOnlyList<OperationTypeResponse>> GetOperationTypeByBaseIdsAsync(int firmId, IReadOnlyCollection<long> documentBaseIds);
    }
}
