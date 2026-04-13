using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.AccountablePersonMoneyPayments;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccountingV2.Client.AccountablePersonMoneyPayments
{
    public interface IAccAccountablePersonPaymentsApiClient : IDI
    {
        Task<long> CreatePaymentForAccountablePersonAsync(int firmId, int userId, PaymentForAccountablePersonDto request);

        Task<MoneyOperationAdditionalDto> GetMoneyOperationAdditionalDataAsync(int firmId, int userId, long? baseId);
    }
}