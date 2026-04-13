using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Finances.Domain.Enums.Money;
using Moedelo.Finances.Domain.Models.Money.Duplicates;
using Moedelo.Finances.Domain.Models.Money.Operations.Duplicates;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Domain.Interfaces.Business.Money
{
    public interface IFinancialOperationsDuplicatesReader : IDI
    {
        Task<int?> GetMaterialAidOperationIdAsync(DuplicateOperationRequest request);
        Task<int?> GetUkInpamentOperationIdAsync(DuplicateOperationRequest request);

        Task<int?> GetIncomingOperationIdAsync(DuplicateOperationRequest request);
        Task<int?> GetOutgoingOperationIdAsync(DuplicateOperationRequest request);

        Task<List<OperationDuplicate>> GetAllOperationsDuplicateAsync(DuplicateOperationRequest request, 
            FinancialOperationDirection direction);

        Task<int?> GetBudgetaryPaymentOperationIdAsync(DuplicateBudgetaryPaymentOperationRequest request);
        Task<int?> GetDividendPaymentOperationIdAsync(DuplicateDividendPaymentOperationRequest request);

        Task<int?> GetMovementOperationIdAsync(DuplicateMovementOperationRequest request);

        Task<int?> GetRoboAndSapeIncomingOperationIdAsync(DuplicateOperationRequest request);
        Task<int?> GetRoboAndSapeOutgoingOperationIdAsync(DuplicateOperationRequest request);

        Task<int?> GetYandexIncomingOperationIdAsync(DuplicateOperationRequest request);
        Task<int?> GetYandexOutgoingOperationIdAsync(DuplicateOperationRequest request);
        Task<int?> GetYandexMovementOperationIdAsync(DuplicateOperationRequest request);
    }
}