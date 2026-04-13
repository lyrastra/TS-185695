using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Finances.Domain.Models.Money.Duplicates;
using Moedelo.Finances.Domain.Models.Money.Operations.Duplicates;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Domain.Interfaces.DataAccess.FinancialOperations
{
    public interface IFinancialOperationsDuplicatesDao : IDI
    {
        Task<int?> GetOperationIdAsync(DuplicateOperationRequest request);

        Task<int?> GetMaterialAidOperationIdAsync(DuplicateOperationRequest request);
        Task<int?> GetUkInpamentOperationIdAsync(DuplicateOperationRequest request);

        Task<int?> GetDividendPaymentOperationIdAsync(DuplicateDividendPaymentOperationRequest request);
        Task<int?> GetBudgetaryPaymentOperationIdAsync(DuplicateBudgetaryPaymentOperationRequest request);

        Task<int?> GetMovementOperationIdAsync(DuplicateMovementOperationRequest request);

        Task<int?> GetRoboAndSapeIncomingOperationIdAsync(DuplicateOperationRequest request);
        Task<int?> GetRoboAndSapeOutgoingOperationIdAsync(DuplicateOperationRequest request);

        Task<int?> GetYandexOperationIdAsync(DuplicateOperationRequest request);
        Task<int?> GetYandexMovementOperationIdAsync(DuplicateOperationRequest request);

        Task<List<OperationDuplicate>> GetAllOperationsAsync(DuplicateOperationRequest request, bool isMovement = false);

        Task<List<OperationDuplicateForBatchCheck>> GetForBatchDetectionAsync(int firmId, DetermineOutgoingOperationsDuplicatesDbRequest request);
    }
}