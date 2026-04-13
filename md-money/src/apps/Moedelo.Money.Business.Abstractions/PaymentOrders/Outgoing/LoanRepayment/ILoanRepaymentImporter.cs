using Moedelo.Money.Domain.PaymentOrders.Outgoing.LoanRepayment;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.LoanRepayment
{
    public interface ILoanRepaymentImporter
    {
        Task ImportAsync(LoanRepaymentImportRequest request);
    }
}