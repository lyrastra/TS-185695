using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Common.DataAccess.Abstractions.UnifiedTaxPayments;
using Moedelo.Money.Common.Domain.Models;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Exceptions;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.UnifiedBudgetaryPayment;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    [InjectAsSingleton(typeof(IUnifiedTaxPaymentReader))]
    public class UnifiedTaxPaymentReader : IUnifiedTaxPaymentReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IUnifiedTaxPaymentDao unifiedTaxPaymentDao;

        public UnifiedTaxPaymentReader(
            IUnifiedTaxPaymentDao unifiedTaxPaymentDao, 
            IExecutionInfoContextAccessor contextAccessor)
        {
            this.unifiedTaxPaymentDao = unifiedTaxPaymentDao;
            this.contextAccessor = contextAccessor;
        }

        public async Task<UnifiedBudgetarySubPayment> GetByBaseIdAsync(long documentBaseId)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var subPayment = await unifiedTaxPaymentDao.GetByBaseIdAsync((int)context.FirmId, documentBaseId);

            if (subPayment == null)
            {
                throw new PaymentOrderNotFoundExcepton(documentBaseId);
            }

            return subPayment;
        }

        public async Task<IReadOnlyCollection<UnifiedBudgetarySubPayment>> GetByBaseIdsAsync(IReadOnlyCollection<long> documentBaseIds, CancellationToken ct)
        {
            var context = contextAccessor.ExecutionInfoContext;
            return await unifiedTaxPaymentDao.GetByBaseIdsAsync((int)context.FirmId, documentBaseIds, ct);
        }

        public async Task<IReadOnlyCollection<UnifiedBudgetarySubPayment>> GetByParentBaseIdsAsync(
            IReadOnlyCollection<long> parentBaseIds)
        {
            var context = contextAccessor.ExecutionInfoContext;
            return await unifiedTaxPaymentDao.GetByParentBaseIdsAsync((int)context.FirmId, parentBaseIds);
        }
    }
}