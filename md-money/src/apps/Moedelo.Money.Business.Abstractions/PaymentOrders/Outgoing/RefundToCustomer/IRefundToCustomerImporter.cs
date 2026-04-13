using Moedelo.Money.Domain.PaymentOrders.Outgoing.RefundToCustomer;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.RefundToCustomer
{
    public interface IRefundToCustomerImporter
    {
        Task ImportAsync(RefundToCustomerImportRequest request);
    }
}