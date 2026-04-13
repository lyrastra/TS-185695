using System;
using System.Threading.Tasks;
using Moedelo.PaymentOrderImport.ApiClient.Abstractions.Dto;

namespace Moedelo.PaymentOrderImport.ApiClient.Abstractions.Reconciliation;

public interface IReconciliationResultApiClient
{
    Task InsertAsync(ReconciliationResultDto dto);

    Task UpdateAsync(ReconciliationResultDto dto);

    Task<ReconciliationResultDto> GetAsync(Guid sessionId, int firmId);
}