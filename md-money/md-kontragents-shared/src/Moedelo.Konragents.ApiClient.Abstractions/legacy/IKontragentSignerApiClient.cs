using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Konragents.ApiClient.Abstractions.legacy.Dtos;

namespace Moedelo.Konragents.ApiClient.Abstractions.legacy
{
    // https://github.com/moedelo/md-commonV2/blob/master/src/clients/kontragents/Moedelo.KontragentsV2.Client/Kontragents/IKontragentSignerClient.cs
    public interface IKontragentSignerApiClient
    {
        Task<KontragentSignerDto> GetByKontragentAsync(FirmId firmId, UserId userId, int kontragentId);
    }
}