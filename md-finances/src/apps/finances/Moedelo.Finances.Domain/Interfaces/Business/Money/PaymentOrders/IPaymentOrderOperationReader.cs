using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Finances.Domain.Models.Money.Operations.PaymentOrders;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Domain.Interfaces.Business.Money.PaymentOrders
{
    public interface IPaymentOrderOperationReader : IDI
    {
        Task<List<PaymentOrderOperation>> GetByBaseIdsAsync(int firmId, IReadOnlyCollection<long> baseIds);
    }
}
