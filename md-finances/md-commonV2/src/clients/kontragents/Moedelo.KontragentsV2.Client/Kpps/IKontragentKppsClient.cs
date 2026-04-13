using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.KontragentsV2.Dto;

namespace Moedelo.KontragentsV2.Client.Kpps
{
    public interface IKontragentKppsClient : IDI
    {
        Task<long> SaveAsync(int firmId, int userId, KontragentKppDto kpp);

        Task<KontragentKppDto> GetByKontragentAsync(int firmId, int userId, int kontragentId, DateTime date);

        Task<IList<KontragentKppDto>> GetByKontragentAsync(int firmId, int? userId, int kontragentId);
        
        Task<IList<KontragentKppDto>> GetByKontragentIdsAsync(int firmId, int userId, KontragentKppsRequestDto requestDto);
    }
}