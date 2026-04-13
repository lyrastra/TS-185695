using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.ErptV2.Client.BizToAccTransfer
{
    public interface IErptBizToAccTransferClient : IDI
    {
        Task RollbackEdsAsync(int fromFirmId, int toFirmId);
    }
}
