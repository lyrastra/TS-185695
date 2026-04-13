using System.Collections.Generic;
using System.Threading;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Dto.PaymentOrders;
using Moedelo.Money.Dto.PaymentOrders.Outgoing.PaymentToNaturalPersons;
using System.Threading.Tasks;

namespace Moedelo.Money.Client.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    public interface IPaymentToNaturalPersonsClient : IDI
    {
        Task<PaymentOrderSaveResponseDto> CreateAsync(int firmId, int userId, PaymentToNaturalPersonsSaveDto dto);

        Task<PaymentToNaturalPersonResponseDto> GetAsync(int firmId, int userId, long documentBaseId);

        Task<PaymentToNaturalPersonResponseDto[]> GetByBaseIdsIdAsync(
            int firmId, int userId, 
            IReadOnlyCollection<long> documentBaseIds,
            CancellationToken cancellationToken);
    }
}
