using Moedelo.Money.Domain.PurseOperations.Outgoing.PaymentSystemFee;
using Moedelo.Money.PurseOperations.Dto.PurseOperations.Outgoing;

namespace Moedelo.Money.Business.PurseOperations.Outgoing.PaymentSystemFee
{
    internal static class PaymentSystemFeeMapper
    {
        internal static PaymentSystemFeeResponse MapToResponse(PaymentSystemFeeDto dto)
        {
            return new PaymentSystemFeeResponse
            {
                Date = dto.Date
            };
        }
    }
}
