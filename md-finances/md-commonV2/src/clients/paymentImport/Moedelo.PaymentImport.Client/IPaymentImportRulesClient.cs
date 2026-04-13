using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.PaymentImport.Dto;

namespace Moedelo.PaymentImport.Client
{
    public interface IPaymentImportRulesClient : IDI
    {
        Task<IdentifyOperationResponseDto[]> IdentifyOperationsAsync(int firmId, int userId, IReadOnlyCollection<IdentifyOperationRequestDto> request);
        Task<IdentifyOperationTaxationSystemResponseDto[]> IdentifyTaxationSystemAsync(int firmId, int userId, IReadOnlyCollection<IdentifyOperationTaxationSystemRequestDto> request);

        Task<IdentifyOperationIgnoreNumberResponseDto[]> IdentifyIgnoreNumberAsync(int firmId, int userId, IReadOnlyCollection<IdentifyOperationIgnoreNumberRequestDto> request);

        Task<IReadOnlyCollection<AppliedImportRuleDto>> GetAppliedRulesAsync(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds);
    }
}
