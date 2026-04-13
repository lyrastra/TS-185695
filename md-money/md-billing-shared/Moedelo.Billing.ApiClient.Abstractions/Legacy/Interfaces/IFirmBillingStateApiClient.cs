using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.Legacy.Dto;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Billing.Abstractions.Legacy.Interfaces;

public interface IFirmBillingStateApiClient
{
    Task<FirmBillingStateDto> GetActualAsync(FirmId firmId);

    Task<IReadOnlyCollection<FirmBillingStateDto>> GetActualListAsync(
        IReadOnlyCollection<int> firmIds,
        HttpQuerySetting httpQuerySetting = null,
        CancellationToken cancellationToken = default);
}