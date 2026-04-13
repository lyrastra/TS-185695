using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.BudgetaryPayment;
using Moedelo.Money.PaymentOrders.DataAccess.Abstractions;
using Moedelo.Money.PaymentOrders.Domain.Models.BudgetaryPayment;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Outgoing.BudgetaryPayment
{
    [InjectAsSingleton(typeof(ICurrencyInvoiceNdsPaymentsReader))]
    class CurrencyInvoiceNdsPaymentsReader : ICurrencyInvoiceNdsPaymentsReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly ICurrencyInvoiceNdsPaymentsDao dao;

        public CurrencyInvoiceNdsPaymentsReader(
            ICurrencyInvoiceNdsPaymentsDao dao, 
            IExecutionInfoContextAccessor contextAccessor)
        {
            this.dao = dao;
            this.contextAccessor = contextAccessor;
        }

        public Task<IReadOnlyList<CurrencyInvoiceNdsPayment>> GetByCriteriaAsync(CurrencyInvoiceNdsPaymentsRequest request)
        {
            var context = contextAccessor.ExecutionInfoContext;
            return dao.GetByCriteriaAsync((int)context.FirmId, request);
        }
    }
}