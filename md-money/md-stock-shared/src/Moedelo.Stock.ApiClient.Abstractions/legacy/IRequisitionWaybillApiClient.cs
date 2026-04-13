using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.FifoSelfCost;

namespace Moedelo.Stock.ApiClient.Abstractions.legacy
{
    public interface IRequisitionWaybillApiClient
    {
        Task<IReadOnlyCollection<RequisitionWaybillFifoSelfCostSourceDto>> GetSelfCostSourcesAsync(FirmId firmId, UserId userId, SelfCostSourceRequestDto dto);
    }
}