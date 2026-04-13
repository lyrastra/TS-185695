using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.Konragents.ApiClient.Abstractions.legacy.Dtos;

namespace Moedelo.Konragents.ApiClient.Abstractions.legacy
{
    public interface IKontragentDebtApiClient
    {
        Task UpdateDebtsAsync(FirmId firmId, UserId userId, UpdateKontragentsDebtsDto request, HttpQuerySetting setting = null);

        Task<decimal> GetKontragentDebtAsync(FirmId firmId, UserId userId, int kontragentId, HttpQuerySetting setting = null);
    }
}