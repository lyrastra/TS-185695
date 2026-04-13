using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy
{
    public interface ITaxationSystemApiClient
    {
        Task<ActualFirmTaxationSystemDto> GetActualAsync(
            int firmId, 
            CancellationToken cancelationToken = default);

        Task<IReadOnlyCollection<ActualFirmTaxationSystemDto>> GetActualAsync(
            IReadOnlyCollection<int> firmIds,
            CancellationToken cancellationToken, 
            HttpQuerySetting setting = null);

        Task<TaxationSystemDto[]> GetAsync(FirmId firmId, UserId userId);

        Task<IReadOnlyCollection<FirmTaxationSystemDto>> GetAsync(IReadOnlyCollection<int> firmIds);

        Task<TaxationSystemDto> GetByYearAsync(FirmId firmId, UserId userId, int year);

        Task SaveAsync(FirmId firmId, UserId userId, TaxationSystemDto taxationSystem);
    }
}