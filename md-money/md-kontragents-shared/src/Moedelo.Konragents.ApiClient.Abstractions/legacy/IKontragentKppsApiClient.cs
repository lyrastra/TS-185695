using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Konragents.ApiClient.Abstractions.legacy.Dtos;

namespace Moedelo.Konragents.ApiClient.Abstractions.legacy
{
    // https://github.com/moedelo/md-commonV2/blob/master/src/clients/kontragents/Moedelo.KontragentsV2.Client/Kpps/IKontragentKppsClient.cs
    public interface IKontragentKppsApiClient
    {
        Task<KontragentKppDto> GetByKontragentAsync(FirmId firmId, UserId userId, int kontragentId, DateTime date);

        Task<IReadOnlyList<KontragentKppDto>> GetByKontragentIdsAsync(FirmId firmId, UserId userId,
            KontragentKppsRequestDto request);

        Task<long> SaveAsync(FirmId firmId, UserId userId, KontragentKppDto kpp);

        Task<IList<KontragentKppDto>> GetByKontragentAsync(FirmId firmId, UserId? userId, int kontragentId);
    }
}