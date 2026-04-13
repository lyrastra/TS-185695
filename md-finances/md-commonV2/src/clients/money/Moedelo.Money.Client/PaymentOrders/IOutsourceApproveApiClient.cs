using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Dto.PaymentOrders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Client.PaymentOrders
{
    public interface IOutsourceApproveApiClient
    {
        Task<OutsourceApproveDto[]> GetIsApprovedAsync(
            int firmId, int userId, IReadOnlyCollection<long> documentBaseIds);

    }
}
