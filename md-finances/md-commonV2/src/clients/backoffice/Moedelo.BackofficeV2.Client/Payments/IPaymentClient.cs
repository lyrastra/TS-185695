using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BackofficeV2.Dto.CreateBill;
using Moedelo.BackofficeV2.Dto.Payments;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.BackofficeV2.Client.Payments
{
    public interface IPaymentClient : IDI
    {
        Task<List<OperationTypeDto>> GetOperationTypesAsync(int firmId);

        Task<List<PriceListCalculationDto>> GetPriceListCalculationsAsync(IEnumerable<PriceListCalculationRequestDto> calculationRequestDtos);
    }
}