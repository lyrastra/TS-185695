using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.PaymentCalendar;
using System.Threading.Tasks;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy
{
    public interface IPaymentCalendarClient
    {
        Task<PaymentCalendarInitialDataDto> GetInitialDataAsync(int firmId, int userId);
    }
}