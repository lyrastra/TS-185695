using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Edm.Base.Client.EdmVersion
{
    public interface IEdmVersionApiClient
    {
        Task<int> GetEdmVersionAsync(int firmId, int userId, CancellationToken cancellationToken = default);
    }
}