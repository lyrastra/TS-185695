using System.Threading.Tasks;
using Moedelo.Common.Types;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy
{
    public interface IAuthorizedCapitalApiClient
    {
        Task<decimal> GetShareByKontragentAsync(FirmId firmId, UserId userId, int kontragentId);
    }
}
