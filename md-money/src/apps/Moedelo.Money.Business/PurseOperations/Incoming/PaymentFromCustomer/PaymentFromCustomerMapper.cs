using Moedelo.Money.Domain.PurseOperations.Incoming.PaymentFromCustomer;
using Moedelo.Money.PurseOperations.Dto.PurseOperations.Incoming;

namespace Moedelo.Money.Business.PurseOperations.Incoming.PaymentFromCustomer
{
    internal static class PaymentFromCustomerMapper
    {
        internal static PaymentFromCustomerResponse MapToResponse(PaymentFromCustomerDto dto)
        {
            return new PaymentFromCustomerResponse
            {
                Date = dto.Date
            };
        }
    }
}
