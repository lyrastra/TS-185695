using Moedelo.Money.Domain.PaymentOrders.Incoming.LoanObtaining;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.LoanObtaining
{
    public interface ILoanObtainingImporter
    {
        Task ImportAsync(LoanObtainingImportRequest request);
    }
}