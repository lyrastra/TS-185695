using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.RequisitesV2.Client.Pfr
{
    public interface IPfrClient : IDI
    {
        Task<bool> IsPfrRequisitesChanged(int firmId, int userId, bool havePfrSignature, bool edsComplete);
    }
}
