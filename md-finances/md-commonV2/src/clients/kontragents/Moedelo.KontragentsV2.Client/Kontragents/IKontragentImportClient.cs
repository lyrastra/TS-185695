using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.KontragentsV2.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.KontragentsV2.Client.Kontragents
{
    public interface IKontragentImportClient : IDI
    {
        Task<int> CreateAsync(int firmId, int userId, KontragentImportDto dto);

        Task UpdateAsync(int firmId, int userId, KontragentImportDto dto);

        Task<List<KontragentImportDto>> GetAsync(int firmId, int userId, Guid guid);

        Task DeleteAsync(int firmId, int userId, Guid guid);
    }
}
