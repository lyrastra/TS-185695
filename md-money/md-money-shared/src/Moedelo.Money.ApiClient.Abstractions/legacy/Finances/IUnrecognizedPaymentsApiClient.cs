using System.Threading.Tasks;
using Moedelo.Money.ApiClient.Abstractions.legacy.Finances.Dtos.UnrecognizedPayments;

namespace Moedelo.Money.ApiClient.Abstractions.legacy.Finances
{
    public interface IUnrecognizedPaymentsApiClient
    {
        Task<UnrecognizedMoneyTableResponseDto> GetUnrecognizedTableAsync(int firmId, int userId, MoneyTableRequestDto request);
    }
}
