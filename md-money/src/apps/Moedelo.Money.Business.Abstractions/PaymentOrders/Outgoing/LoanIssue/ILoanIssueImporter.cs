using Moedelo.Money.Domain.PaymentOrders.Outgoing.LoanIssue;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.LoanIssue
{
    public interface ILoanIssueImporter
    {
        Task ImportAsync(LoanIssueImportRequest request);
    }
}