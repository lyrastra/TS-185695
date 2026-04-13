using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.CashOrders.Business.Abstractions;
using Moedelo.Money.CashOrders.Business.Abstractions.Models;
using Moedelo.Money.CashOrders.Business.Abstractions.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Common.DataAccess.Abstractions.UnifiedTaxPayments;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.CashOrders.Business.CashOrders.Outgoing.UnifiedBudgetaryPayment
{
    [InjectAsSingleton(typeof(IUnifiedBudgetaryPaymentReader))]

    class UnifiedBudgetaryPaymentReader : IUnifiedBudgetaryPaymentReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly ICashOrderReader cashOrderReader;
        private readonly IUnifiedTaxPaymentDao unifiedTaxPaymentDao;

        public UnifiedBudgetaryPaymentReader(
            IExecutionInfoContextAccessor contextAccessor,
            ICashOrderReader cashOrderReader,
            IUnifiedTaxPaymentDao unifiedTaxPaymentDao)
        {
            this.contextAccessor = contextAccessor;
            this.cashOrderReader = cashOrderReader;
            this.unifiedTaxPaymentDao = unifiedTaxPaymentDao;
        }

        public async Task<CashOrderResponse> GetByBaseIdAsync(long documentBaseId)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var response = await cashOrderReader.GetByBaseIdAsync(documentBaseId, OperationType.CashOrderOutgoingUnifiedBudgetaryPayment);
            response.UnifiedBudgetarySubPayments = await unifiedTaxPaymentDao.GetByParentBaseIdAsync((int)context.FirmId, documentBaseId);
            return response;
        }
    }
}
