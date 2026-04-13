using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.RptV2.Dto.TradingTax;
using System.Threading.Tasks;

namespace Moedelo.RptV2.Client.TradingTax
{
    public interface ITradingTaxPaymentClient : IDI
    {
        Task<TradingTaxPaymentDto> GetPaymentDataAsync(int firmId, int userId, int eventId);
    }
}
