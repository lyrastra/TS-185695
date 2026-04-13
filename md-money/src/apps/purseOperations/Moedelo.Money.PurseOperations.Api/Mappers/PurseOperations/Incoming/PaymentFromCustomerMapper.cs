using Moedelo.Money.PurseOperations.Business.Abstractions.Models;
using Moedelo.Money.PurseOperations.Dto.PurseOperations.Incoming;

namespace Moedelo.Money.PurseOperations.Api.Mappers.PurseOperations.Incoming
{
    internal class PaymentFromCustomerMapper
    {
        public static PaymentFromCustomerDto Map(PurseOperationResponse model)
        {
            return new PaymentFromCustomerDto
            {
                Date = model.PurseOperation.Date
            };
        }
    }
}
