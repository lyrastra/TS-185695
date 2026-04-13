using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Money;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Client
{
    public interface IMoneyApiClient : IDI
    {
        Task<List<OperationResponseDto>> GetOperationsAsync(
            int firmId,
            int userId,
            TaxationSystemType taxationSystemType,
            PaymentSources? source = null);
        
        Task<List<OperationResponseDto>> GetOperationsAsync(int firmId, int userId, RegistryQueryDto request);

        Task<List<RentPaymentPeriodDto>> GetByPaymentBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> ids);
    }
}
