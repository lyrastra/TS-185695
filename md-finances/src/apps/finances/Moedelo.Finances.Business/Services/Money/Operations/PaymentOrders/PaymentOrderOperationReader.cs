using Moedelo.Finances.Domain.Interfaces.Business.Money.PaymentOrders;
using Moedelo.Finances.Domain.Interfaces.DataAccess.Money.Operations;
using Moedelo.Finances.Domain.Models.Money.Operations.PaymentOrders;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Finances.Business.Services.Money.Operations.PaymentOrders
{
    [InjectAsSingleton]
    public class PaymentOrderOperationReader : IPaymentOrderOperationReader
    {
        private readonly IPaymentOrderOperationDao dao;

        public PaymentOrderOperationReader(IPaymentOrderOperationDao dao)
        {
            this.dao = dao;
        }

        public Task<List<PaymentOrderOperation>> GetByBaseIdsAsync(int firmId, IReadOnlyCollection<long> baseIds)
        {
            return dao.GetByBaseIdsAsync(firmId, baseIds);
        }
    }
}