using Moedelo.Money.Domain.Operations;
using Moedelo.Money.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.CashOrders
{
    public interface ICashOrderGetter
    {
        Task<OperationType> GetOperationTypeAsync(long documentBaseId);

        Task<OperationTypeResponse[]> GetOperationTypeByBaseIdsAsync(IReadOnlyCollection<long> documentBaseIds);

        Task<long> GetOperationIdAsync(long documentBaseId);
    }
}
