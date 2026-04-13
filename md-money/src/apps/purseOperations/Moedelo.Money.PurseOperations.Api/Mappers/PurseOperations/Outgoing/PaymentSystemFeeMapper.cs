using Moedelo.Money.PurseOperations.Business.Abstractions.Models;
using Moedelo.Money.PurseOperations.Dto.PurseOperations.Outgoing;

namespace Moedelo.Money.PurseOperations.Api.Mappers.PurseOperations.Outgoing
{
    internal class PaymentSystemFeeMapper
    {
        public static PaymentSystemFeeDto Map(PurseOperationResponse model)
        {
            return new PaymentSystemFeeDto
            {
                Date = model.PurseOperation.Date
            };
        }
    }
}
