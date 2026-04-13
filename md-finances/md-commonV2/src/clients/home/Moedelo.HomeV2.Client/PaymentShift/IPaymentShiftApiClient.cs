using Moedelo.HomeV2.Dto.PaymentShift;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Threading.Tasks;

namespace Moedelo.HomeV2.Client.PaymentShift
{
    public interface IPaymentShiftApiClient : IDI
    {
        Task ShiftPaymentAsync(PaymentShiftRequestDto dto);
    }
}
