using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Konragents.ApiClient.Abstractions.legacy.Dtos;

namespace Moedelo.Konragents.ApiClient.Abstractions.legacy
{
    // https://github.com/moedelo/md-commonV2/blob/master/src/clients/kontragents/Moedelo.KontragentsV2.Client/Kontragents/IKontragentContactClient.cs
    public interface IKontragentContactApiClient
    {
        Task<List<KontragentContactDto>> GetByKontragentsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<int> ids);
    }
}