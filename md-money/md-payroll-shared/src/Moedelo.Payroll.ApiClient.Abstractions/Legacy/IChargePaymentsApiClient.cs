using Moedelo.Common.Types;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargePayments;
using Moedelo.Payroll.Enums.Worker;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy
{
    public interface IChargePaymentsApiClient
    {
        Task<WorkerChargePaymentsListDto> GetByDocumentBaseIdAsync(FirmId firmId, UserId userId, long documentBaseId);

        Task<WorkerChargePaymentsListDto[]> GetByDocumentBaseIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> documentBaseIds);

        Task<WorkerChargePaymentsListDto> GetUnboundForWorkerAsync(FirmId firmId, UserId userId, long? documentBaseId, int workerId, WorkerPaymentType workerPaymentType);

        /// <summary>
        /// Привязывает начисления мастера выплат к созданным из него платежам
        /// </summary>
        Task BindPaymentEventChargePaymentsAsync(FirmId firmId, UserId userId, BindPaymentRequestDto chargePayments);
    }
}