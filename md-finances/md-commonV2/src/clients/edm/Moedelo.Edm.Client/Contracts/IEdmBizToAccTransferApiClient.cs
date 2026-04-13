using System.Threading.Tasks;
using Moedelo.Edm.Dto.BizToAccTransfer;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Edm.Client.Implementations
{
    public interface IEdmBizToAccTransferApiClient : IDI
    {
        Task TransferAsync(EdmInviteTransferDto dto);
        Task SyncEdmAccToBizAsync(SyncEdmAccToBizDto dto);
    }
}