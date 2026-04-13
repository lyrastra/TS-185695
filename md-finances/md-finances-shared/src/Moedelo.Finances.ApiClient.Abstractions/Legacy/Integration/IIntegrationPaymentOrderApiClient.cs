using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Finances.ApiClient.Abstractions.Legacy.Integration.Dto;

namespace Moedelo.Finances.ApiClient.Abstractions.Legacy.Integration
{
    public interface IIntegrationPaymentOrderApiClient
    {
        Task<SendPaymentOrdersResponseDto> SendAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> documentBaseIds);
    }
}
