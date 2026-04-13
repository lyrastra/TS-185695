using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.PaymentEvents;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.PaymentEvents.GenerateEventFiles;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy;

public interface IPaymentEventsApiClient
{
    Task<PaymentEventInitialDataResponseDto> GetInitialDataAsync(FirmId firmId, UserId userId,
        PaymentEventInitialDataRequestDto request);

    Task<IReadOnlyList<EventFilesGroupDto>> GenerateEventFilesAsync(FirmId firmId, UserId userId,
        EventFilesRequestDto request);
}