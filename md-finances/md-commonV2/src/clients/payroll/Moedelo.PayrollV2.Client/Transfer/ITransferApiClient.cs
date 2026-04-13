using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.PayrollV2.Dto.Transfer;

namespace Moedelo.PayrollV2.Client.Transfer
{
    public interface ITransferApiClient:IDI
    {
        Task<Dictionary<int, TransferWorkerDto>> TransferWorkersAsync(TransferWorkersRequestDto request, TimeSpan? timeout = null);
        Task TransferSalarySettingsAsync(int bizUserId, int bizFirmId, int accFirmId);
        Task TransferFiredPaymentsAsync(int bizFirmId, int bizUserId, int accFirmId, int accUserId, HttpQuerySetting setting = null);
    }
}