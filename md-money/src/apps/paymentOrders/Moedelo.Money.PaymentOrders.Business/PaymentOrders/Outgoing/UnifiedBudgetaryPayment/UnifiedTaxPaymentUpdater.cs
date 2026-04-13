using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Common.DataAccess.Abstractions.UnifiedTaxPayments;
using Moedelo.Money.Common.Domain.Models;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Exceptions;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.UnifiedBudgetaryPayment;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    [InjectAsSingleton(typeof(IUnifiedTaxPaymentUpdater))]
    public class UnifiedTaxPaymentUpdater : IUnifiedTaxPaymentUpdater
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IUnifiedTaxPaymentDao unifiedTaxPaymentDao;

        public UnifiedTaxPaymentUpdater(
            IUnifiedTaxPaymentDao unifiedTaxPaymentDao, 
            IExecutionInfoContextAccessor contextAccessor)
        {
            this.unifiedTaxPaymentDao = unifiedTaxPaymentDao;
            this.contextAccessor = contextAccessor;
        }

        public Task SetTaxPostingTypeAsync(long documentBaseId, ProvidePostingType taxPostingType)
        {
            var context = contextAccessor.ExecutionInfoContext;
            return unifiedTaxPaymentDao.UpdateTaxPostingTypeAsync((int)context.FirmId, documentBaseId, taxPostingType);
        }
    }
}