using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Threading.Tasks;

namespace Moedelo.AccountingV2.Client.Preview
{
    public interface IPreviewApiClient : IDI
    {
        Task<byte[]> GetBillOnlineAsync(int id, string guid, bool useWatermark = false);
    }
}
