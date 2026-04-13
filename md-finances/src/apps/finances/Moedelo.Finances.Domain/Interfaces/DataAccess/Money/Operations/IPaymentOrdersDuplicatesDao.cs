using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Finances.Domain.Models.Money.Duplicates;
using Moedelo.Finances.Domain.Models.Money.Operations.Duplicates;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Domain.Interfaces.DataAccess.Money.Operations
{
    public interface IPaymentOrdersDuplicatesDao : IDI
    {
        Task<int?> GetPaymentOrderIdAsync(DuplicateOperationRequest request);

        Task<List<OperationDuplicate>> GetAllPaymentOrdersAsync(DuplicateOperationRequest request);

        Task<List<OperationDuplicateForBatchCheck>> GetForBatchDetectonAsync(int firmId, DetermineOutgoingOperationsDuplicatesDbRequest request);
    }
}