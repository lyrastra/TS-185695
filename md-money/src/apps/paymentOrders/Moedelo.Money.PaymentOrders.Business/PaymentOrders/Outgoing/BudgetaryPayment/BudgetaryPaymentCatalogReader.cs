using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.BudgetaryPayment;
using Moedelo.Money.PaymentOrders.DataAccess.Abstractions;
using Moedelo.Money.PaymentOrders.Domain.Models.BudgetaryPayment;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Outgoing.BudgetaryPayment
{
    [InjectAsSingleton(typeof(IBudgetaryPaymentCatalogReader))]
    class BudgetaryPaymentCatalogReader : IBudgetaryPaymentCatalogReader
    {
        private readonly IBudgetaryPaymentCatalogDao dao;

        public BudgetaryPaymentCatalogReader(IBudgetaryPaymentCatalogDao dao)
        {
            this.dao = dao;
        }

        public Task<BudgetaryPaymentReason[]> GetPaymentReasonsAsync()
        {
            return dao.GetPaymentReasonsAsync();
        }
    }
}
