using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Requisites;

namespace Moedelo.RptV2.Client.RequisitesMaster
{
    public interface IRequisitesMasterClient
    {
        Task<RequisitesMasterStatus> GetStatusAsync(int firmId, int userId, CancellationToken ct = default);
        Task SetStatusAsync(int firmId, int userId, RequisitesMasterStatus status);
    }
}
