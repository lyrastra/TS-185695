using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.PaymentImport.Dto;

namespace Moedelo.PaymentImport.Client.Reconciliation
{
    public interface IReconciliationResultClient : IDI
    {
        Task InsertAsync(ReconciliationResultDto dto);

        Task UpdateAsync(ReconciliationResultDto dto);

        Task<ReconciliationResultDto> GetAsync(Guid sessionId, int firmId);
    }
}