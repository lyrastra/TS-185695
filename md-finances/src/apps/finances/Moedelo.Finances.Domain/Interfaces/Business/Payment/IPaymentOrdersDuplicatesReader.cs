using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Finances.Domain.Enums.Money;
using Moedelo.Finances.Domain.Models.Money.Duplicates;
using Moedelo.Finances.Domain.Models.Money.Operations.Duplicates;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Domain.Interfaces.Business.Payment
{
    public interface IPaymentOrdersDuplicatesReader : IDI
    {
        Task<int?> GetIncomingPaymentOrderIdAsync(DuplicateOperationRequest request);

        Task<int?> GetOutgoingPaymentOrderIdAsync(DuplicateOperationRequest request);

        Task<List<OperationDuplicate>> GetAllPaymentOrdersAsync(DuplicateOperationRequest request, MoneyDirection direction);
    }
}