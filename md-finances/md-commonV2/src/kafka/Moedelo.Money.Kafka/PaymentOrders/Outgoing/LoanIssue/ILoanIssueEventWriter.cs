using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Kafka.PaymentOrders.Outgoing.LoanIssue.Events;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.LoanIssue
{
    public interface ILoanIssueEventWriter : IDI
    {
        Task WriteAsync(int firmId, int userId, LoanIssueUpdatedMessage message);
    }
}