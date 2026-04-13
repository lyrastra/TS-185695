using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Payroll.Deductions;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.PayrollV2.Client.ChargePayments.DTO;

namespace Moedelo.PayrollV2.Client.Payments
{
    public interface IChargePaymentsApiClient : IDI
    {
        Task<WorkerChargePaymentsListDto> GetByDocumentBaseIdAsync(int firmId, int userId, long documentBaseId);

        Task<WorkerChargePaymentsListDto[]> GetByDocumentBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds);

        Task<ApplyDeductionStatus> GetApplyDeductionStatusAsync(int firmId, int userId, long documentBaseId, DateTime documentDate);
    }
}
