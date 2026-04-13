using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.Legacy.Dto.PaymentMethods;

namespace Moedelo.Billing.Abstractions.Legacy.Interfaces;

public interface IPaymentMethodsApiClient
{
    Task<IReadOnlyCollection<PaymentMethodDto>> GetByCriteriaAsync(PaymentMethodSearchCriteriaDto dto);

    Task<IReadOnlyCollection<PaymentMethodDto>> GetAllAsync();
}