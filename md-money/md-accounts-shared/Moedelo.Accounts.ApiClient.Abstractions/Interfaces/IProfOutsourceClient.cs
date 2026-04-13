using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Accounts.Abstractions.Dto;

namespace Moedelo.Accounts.Abstractions.Interfaces
{
    public interface IProfOutsourceClient
    {
        /// <summary> 
        /// Возвращает информацию об обслуживании у профессионального аутсорсера
        /// </summary>
        Task<ProfOutsourceContextDto> GetOutsourceContext(int firmId, int userId);
        
        Task<AccountDto> GetProfOutsourceForFirmAsync(int firmId, int userId, int slaveFirmId);

        Task<IReadOnlyCollection<FirmOnServiceDto>> GetFirmsOnServiceAsync(int firmId, int userId,
            IReadOnlyCollection<int> slaveFirmIds);

        Task<IReadOnlyCollection<FirmOnServiceDto>> GetProfOutsourceFirmsOnServiceByFirmIdsAsync(
          IReadOnlyCollection<int> firmIds,
          CancellationToken cancellationToken = default);
    }
}