using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.RequisitesV2.Dto.FirmTaxationSystem;

namespace Moedelo.RequisitesV2.Client.FirmTaxationSystem
{
    public interface IFirmTaxationSystemClient
    {
        Task<FirmTaxationSystemDto> GetTaxationSystemAsync(int firmId, int year, CancellationToken cancellationToken = default);
        Task<List<TaxationSystemDto>> GetAsync(int firmId, int userId);
        Task<List<TaxationSystemFirmDto>> GetAsync(IReadOnlyCollection<int> firmIds);
        Task<List<ActualFirmTaxationSystemDto>> GetActualAsync(IReadOnlyCollection<int> firmIds);
        Task<TaxationSystemDto> GetByYearAsync(int firmId, int userId, int year, CancellationToken cancellationToken = default);
        Task SaveAsync(int firmId, int userId, TaxationSystemDto taxationSystem);
        
    }
}