using Moedelo.Money.Domain.PaymentOrders.Incoming.LoanReturn;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.LoanReturn
{
    public interface ILoanReturnImporter
    {
        Task ImportAsync(LoanReturnImportRequest request);
    }
}