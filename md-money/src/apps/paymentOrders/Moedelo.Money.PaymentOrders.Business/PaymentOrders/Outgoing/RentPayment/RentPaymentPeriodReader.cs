using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.RentPayment;
using Moedelo.Money.PaymentOrders.DataAccess.Abstractions;
using Moedelo.Money.PaymentOrders.Domain.Models;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Outgoing.RentPayment
{
    [InjectAsSingleton(typeof(IRentPaymentPeriodReader))]
    internal class RentPaymentPeriodReader : IRentPaymentPeriodReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IRentPaymentPeriodDao dao;

        public RentPaymentPeriodReader(IExecutionInfoContextAccessor contextAccessor, IRentPaymentPeriodDao dao)
        {
            this.contextAccessor = contextAccessor;
            this.dao = dao;
        }

        public Task<IReadOnlyList<RentPaymentPeriod>> GetByPaymentBaseIdsAsync(IReadOnlyCollection<long> ids)
        {
            return dao.GetAsync((int)contextAccessor.ExecutionInfoContext.FirmId, ids);
        }
    }
}
